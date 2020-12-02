using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NUnit3Tests
{
    //The following objects were carried over from the source code and included simply 
    //to allow the solution to build: ValueObject, Entity, LoanTerm, LoanAmount, LoanProduct,
    //LoanRepaymentCalculator, ProductComparer, and MonthlyRepaymentComparison.
    //What is important is MonthlyRepaymentGreaterThanZeroConstraint.
    public abstract class ValueObject
    {
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }
            return ReferenceEquals(left, null) || left.Equals(right);
        }

        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !(EqualOperator(left, right));
        }

        protected abstract IEnumerable<object> GetAtomicValues();

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            ValueObject other = (ValueObject)obj;
            IEnumerator<object> thisValues = GetAtomicValues().GetEnumerator();
            IEnumerator<object> otherValues = other.GetAtomicValues().GetEnumerator();

            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (ReferenceEquals(thisValues.Current, null) ^ ReferenceEquals(otherValues.Current, null))
                {
                    return false;
                }
                if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current))
                {
                    return false;
                }
            }
            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }

        public override int GetHashCode()
        {
            return GetAtomicValues()
             .Select(x => x != null ? x.GetHashCode() : 0)
             .Aggregate((x, y) => x ^ y);
        }

        public ValueObject GetCopy()
        {
            return MemberwiseClone() as ValueObject;
        }
    }

    public abstract class Entity
    {
        int? _requestedHashCode;
        int _Id;
        public virtual int Id
        {
            get
            {
                return _Id;
            }
            protected set
            {
                _Id = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            Entity item = (Entity)obj;

            return item.Id == this.Id;
        }

        public override int GetHashCode()
        {
            if (!_requestedHashCode.HasValue)
                _requestedHashCode = this.Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

            return _requestedHashCode.Value;
        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }

    public class LoanTerm : ValueObject
    {
        public int Years { get; }

        // Explicitly stating to hide dealt constructor to indicate immutability
        private LoanTerm() { }

        public LoanTerm(int years)
        {
            if (years < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(years),
                    "Please specify a value greater than 0.");
            }

            Years = years;
        }

        public int ToMonths() => Years * 12;

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Years;
        }
    }

    public class LoanAmount : ValueObject
    {
        public string CurrencyCode { get; }
        public decimal Principal { get; }

        // Explicitly stating to hide dealt constructor to indicate immutability
        private LoanAmount() { }

        public LoanAmount(string currencyCode, decimal principal)
        {
            CurrencyCode = currencyCode;
            Principal = principal;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return CurrencyCode;
            yield return Principal;
        }
    }

    public class LoanProduct : Entity
    {
        private string _productName;
        private decimal _interestRate;

        protected LoanProduct() { }

        public LoanProduct(int id, string productName, decimal interestRate)
        {
            Id = id;
            _productName = productName;
            _interestRate = interestRate;
        }

        public string GetProductName()
        {
            return _productName;
        }

        public decimal GetInterestRate()
        {
            return _interestRate;
        }
    }

    public class LoanRepaymentCalculator
    {
        public decimal CalculateMonthlyRepayment(LoanAmount loanAmount, decimal annualInterestRate, LoanTerm loanTerm)
        {
            var monthly = (double)annualInterestRate / 100 / 12 * (double)loanAmount.Principal / (1 - Math.Pow(1 + ((double)annualInterestRate / 100 / 12), -loanTerm.ToMonths()));

            return new decimal(Math.Round(monthly, 2, MidpointRounding.AwayFromZero));
        }
    }

    public class ProductComparer
    {
        private readonly LoanAmount _loanAmount;
        private readonly List<LoanProduct> _productsToCompare;

        public ProductComparer(LoanAmount loanAmount, List<LoanProduct> productsToCompare)
        {
            _loanAmount = loanAmount;
            _productsToCompare = productsToCompare;
        }

        public List<MonthlyRepaymentComparison> CompareMonthlyRepayments(LoanTerm loanTerm)
        {
            var calculator = new LoanRepaymentCalculator();

            var compared = new List<MonthlyRepaymentComparison>();

            foreach (var product in _productsToCompare)
            {
                decimal repayment = calculator.CalculateMonthlyRepayment(_loanAmount, product.GetInterestRate(), loanTerm);
                compared.Add(new MonthlyRepaymentComparison(product.GetProductName(), product.GetInterestRate(), repayment));
            }

            return compared;
        }
    }

    public class MonthlyRepaymentComparison : ValueObject
    {
        public string ProductName { get; }
        public decimal InterestRate { get; }
        public decimal MonthlyRepayment { get; }

        private MonthlyRepaymentComparison() { }

        public MonthlyRepaymentComparison(string productName, decimal interestRate, decimal monthlyRepayemt)
        {
            ProductName = productName;
            InterestRate = interestRate;
            MonthlyRepayment = monthlyRepayemt;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return ProductName;
            yield return InterestRate;
            yield return MonthlyRepayment;
        }
    }

    public class MonthlyRepaymentGreaterThanZeroConstraint : Constraint
    {
        public string ExpectedProductName { get; }
        public decimal ExpectedInterestRate { get; }

        public MonthlyRepaymentGreaterThanZeroConstraint(
            string expectedProductName,
            decimal expectedInterestRate
        )
        {
            ExpectedProductName = expectedProductName;
            ExpectedInterestRate = expectedInterestRate;
        }

        public override ConstraintResult ApplyTo<TActual>(TActual actual)
        {
            //This overridden method decides if the Assert passes or fails.
            MonthlyRepaymentComparison comparison = actual as MonthlyRepaymentComparison;

            if (comparison is null)
            {
                return new ConstraintResult(this, actual, ConstraintStatus.Error);
            }

            if (comparison.InterestRate == ExpectedInterestRate &&
                comparison.ProductName == ExpectedProductName &&
                comparison.MonthlyRepayment > 0)
            {
                return new ConstraintResult(this, actual, ConstraintStatus.Success);
            }
            else
            {
                return new ConstraintResult(this, actual, ConstraintStatus.Failure);
            }
        }
    }

    [Category("Custom constraint test")]
    public class ProductComparerShould
    {
        private List<LoanProduct> products;
        private ProductComparer sut;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // Simulate long setup init time for this list of products
            // We assume that this list will not be modified by any tests
            // as this will potentially break other tests (i.e. break test isolation)
            products = new List<LoanProduct>
            {
                new LoanProduct(1, "a", 1),
                new LoanProduct(2, "b", 2),
                new LoanProduct(3, "c", 3)
            };
        }

        [SetUp]
        public void Setup()
        {
            sut = new ProductComparer(new LoanAmount("USD", 200_000m), products);
        }

        [Test]
        public void ReturnComparisonForFirstProduct_WithPartialKnownExpectedValues()
        {
            List<MonthlyRepaymentComparison> comparisons =
                sut.CompareMonthlyRepayments(new LoanTerm(30));

            //The code below was replaced by the custom constraint.
            //Assert.That(
            //  comparisons, 
            //  Has
            //    .Exactly(1)
            //    .Matches<MonthlyRepaymentComparison>(
            //            item => item.ProductName == "a" &&
            //                    item.InterestRate == 1 &&
            //                    item.MonthlyRepayment > 0));

            Assert.That(
                comparisons,
                Has
                    .Exactly(1)
                    .Matches(new MonthlyRepaymentGreaterThanZeroConstraint("a", 1)));

        }
    }
}
