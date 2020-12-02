using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace NUnit3Tests
{
    [Category("TestCase tests")]
    public class TestCaseTests
    {
        private abstract class ValueObject
        {
            protected static bool EqualOperator(ValueObject left, ValueObject right)
            {
                if (left is null ^ right is null)
                {
                    return false;
                }
                return left is null || left.Equals(right);
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
                    if (thisValues.Current is null ^ otherValues.Current is null)
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

        private class LoanTerm : ValueObject
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

        private class LoanAmount : ValueObject
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

        private class LoanRepaymentCalculator
        {
            public decimal CalculateMonthlyRepayment(LoanAmount loanAmount, decimal annualInterestRate, LoanTerm loanTerm)
            {
                var monthly = (double)annualInterestRate / 100 / 12 * (double)loanAmount.Principal / (1 - Math.Pow(1 + ((double)annualInterestRate / 100 / 12), -loanTerm.ToMonths()));

                return new decimal(Math.Round(monthly, 2, MidpointRounding.AwayFromZero));
            }
        }

        private class MonthlyRepaymentTestData
        {
            //This provides a centralized place for NUnit to pull test data from.
            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(200_000m, 6.5m, 30, 1264.14m);
                    yield return new TestCaseData(500_000m, 10m, 30, 4387.86m);
                    yield return new TestCaseData(200_000m, 10m, 30, 1755.14m);
                }
            }
        }

        private class MonthlyRepaymentTestDataWithReturn
        {
            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(200_000m, 6.5m, 30).Returns(1264.14);
                    yield return new TestCaseData(200_000m, 10m, 30).Returns(1755.14);
                    yield return new TestCaseData(500_000m, 10m, 30).Returns(4387.86);
                }
            }
        }

        private class MonthlyRepaymentCsvData
        {
            public static IEnumerable GetTestCases(string csvFileName)
            {
                var csvLines = File.ReadAllLines(csvFileName);

                var testCases = new List<TestCaseData>();

                foreach (var line in csvLines)
                {
                    string[] values = line.Replace(" ", "").Split(',');

                    decimal principal = decimal.Parse(values[0]);
                    decimal interestRate = decimal.Parse(values[1]);
                    int termInYears = int.Parse(values[2]);
                    decimal expectedRepayment = decimal.Parse(values[3]);

                    testCases.Add(new TestCaseData(principal, interestRate, termInYears, expectedRepayment));
                }

                return testCases;
            }
        }

        [Category("Method level data tests")]
        public class LoanRepaymentCalculatorShould
        {
            [Test]
            [TestCase(200_000, 6.5, 30, 1264.14)]
            [TestCase(200_000, 10, 30, 1755.14)]
            [TestCase(500_000, 10, 30, 4387.86)]
            [Category("TestCase data tests")]
            public void CalculateCorrectMonthlyRepayment(
                decimal principal,
                decimal interestRate,
                int termInYears,
                decimal expectedMonthlyPayment
            )
            {
                var sut = new LoanRepaymentCalculator();

                var monthlyPayment =
                    sut.CalculateMonthlyRepayment(
                        new LoanAmount("USD", principal),
                        interestRate,
                        new LoanTerm(termInYears)
                    );

                Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));
            }

            [Test]
            [TestCase(200_000, 6.5, 30, ExpectedResult = 1264.14)]
            [TestCase(200_000, 10, 30, ExpectedResult = 1755.14)]
            [TestCase(500_000, 10, 30, ExpectedResult = 4387.86)]
            [Category("TestCase data tests")]
            public decimal CalculateCorrectMonthlyRepaymentSimplified(
                decimal principal,
                decimal interestRate,
                int termInYears
            )
            {
                var sut = new LoanRepaymentCalculator();

                return
                    sut.CalculateMonthlyRepayment(
                        new LoanAmount("USD", principal),
                        interestRate,
                        new LoanTerm(termInYears)
                    );
            }

            [Test]
            [TestCaseSource(typeof(MonthlyRepaymentTestData), "TestCases")]
            [Category("TestCaseSource data tests")]
            public void CalculateCorrectMonthlyRepayment_Centralized(
                decimal principal,
                decimal interestRate,
                int termInYears,
                decimal expectedMonthlyPayment
            )
            {
                var sut = new LoanRepaymentCalculator();

                var monthlyPayment =
                    sut.CalculateMonthlyRepayment(
                        new LoanAmount("USD", principal),
                        interestRate,
                        new LoanTerm(termInYears)
                    );

                Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));
            }

            [Test]
            [TestCaseSource(typeof(MonthlyRepaymentTestDataWithReturn), "TestCases")]
            [Category("TestCaseSource data tests")]
            public decimal CalculateCorrectMonthlyRepayment_CentralizedWithReturn(
                decimal principal,
                decimal interestRate,
                int termInYears
            )
            {
                var sut = new LoanRepaymentCalculator();

                return
                    sut.CalculateMonthlyRepayment(
                        new LoanAmount("USD", principal),
                        interestRate,
                        new LoanTerm(termInYears)
                    );
            }

            [Test]
            [TestCaseSource(typeof(MonthlyRepaymentCsvData), "GetTestCases", new object[] { @".\DataFiles\Data.csv" })]
            [Category("TestCaseSource data tests")]
            public void CalculateCorrectMonthlyRepayment_Csv(
                decimal principal,
                decimal interestRate,
                int termInYears,
                decimal expectedMonthlyPayment
            )
            {
                var sut = new LoanRepaymentCalculator();

                var monthlyPayment =
                    sut.CalculateMonthlyRepayment(
                        new LoanAmount("USD", principal),
                        interestRate,
                        new LoanTerm(termInYears)
                    );

                Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));
            }

            [Test]
            [Combinatorial]
            [Category("Values data tests")]
            public void CalculateCorrectMonthlyRepayment_Combinatorial(
                [Values(100_000, 200_000, 500_000)] decimal principal,
                [Values(6.5, 10, 20)] decimal interestRate,
                [Values(10, 20, 30)] int termInYears)
            {
                var sut = new LoanRepaymentCalculator();

                var monthlyPayment = sut.CalculateMonthlyRepayment(
                    new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));
            }

            [Test]
            [Sequential]
            [Category("Values data tests")]
            public void CalculateCorrectMonthlyRepayment_Sequential(
                        [Values(200_000, 200_000, 500_000)] decimal principal,
                        [Values(6.5, 10, 10)] decimal interestRate,
                        [Values(30, 30, 30)] int termInYears,
                        [Values(1264.14, 1755.14, 4387.86)] decimal expectedMonthlyPayment)
            {
                //This test is essentially the same as having a test method with three TestCase attributes.
                var sut = new LoanRepaymentCalculator();

                var monthlyPayment = sut.CalculateMonthlyRepayment(
                    new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));

                Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));
            }
        }

        [Test]
        [Category("Values data tests")]
        public void CalculateCorrectMonthlyRepayment_Range(
            [Range(50_000, 1_000_000, 50_000)] decimal principal,
            [Range(0.5, 20.00, 0.5)] decimal interestRate,
            [Values(10, 20, 30)] int termInYears)
        {
            //The [Range] attribute above works in the following format: starting value, ending value, step.
            var sut = new LoanRepaymentCalculator();

            //The point of this test is to see if any combination of properties 
            //causes the CalculateMonthlyRepayment() to throw an error.
            sut.CalculateMonthlyRepayment(
                new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));
        }

        [Test, Pairwise]
        [Category("Values data tests")]
        public void ValidateLandingSiteOfRover_When_GoingToMars(
            [Values("a", "b", "c")] string a,
            [Values("+", "-")] string b,
            [Values("x", "y")] string c
        )
        {
            Debug.WriteLine("{0} {1} {2}", a, b, c);
        }


        [Test]
        [Category("Values data tests")]
        public void GenerateRandomLandingSiteOnMoon(
            [Values(1, 2, 3)] int x,
            [Random(-1.0, 1.0, 5)] double d
        )
        {
            //The following test will be executed fifteen times, three times for each value of x, 
            //each combined with 5 random doubles from - 1.0 to + 1.0.
            Debug.WriteLine("{0} {1}", x, d);
        }
    }
}
