using NUnit.Framework;
using System.Collections.Generic;

namespace NUnit3Tests
{
    [Category("Comparison tests")]
    public class ComparisonTests
    {
        private readonly int result = 42;
        private readonly bool meaningOfLife = 42 > 0;

        [Test]
        public void GreaterThanTest()
        {
            //GreaterThanConstraint tests that one value is greater than another.

            //Modifiers
            //...Using(IComparer comparer)
            //...Using<T>(IComparer<T> comparer)
            //...Using<T>(Comparison<T> comparer)

            Assert.That(result, Is.GreaterThan(2));
            Assert.That(7, Is.GreaterThan(3));
            //Assert.That(myOwnObject, Is.GreaterThan(theExpected).Using(myComparer));
        }

        [Test]
        public void GreaterThanOrEqualTest()
        {
            //GreaterThanOrEqualConstraint tests that one value is greater than or equal to another.

            //Modifiers
            //...Using(IComparer comparer)
            //...Using<T>(IComparer<T> comparer)
            //...Using<T>(Comparison<T> comparer)

            Assert.That(result, Is.GreaterThanOrEqualTo(42));
            Assert.That(7, Is.GreaterThanOrEqualTo(3));
            Assert.That(7, Is.AtLeast(3));
            Assert.That(7, Is.GreaterThanOrEqualTo(7));
            Assert.That(7, Is.AtLeast(7));
            //Assert.That(myOwnObject, Is.GreaterThanOrEqualTo(theExpected).Using(myComparer));
        }

        [Test]
        public void LessThanTest()
        {
            //LessThanConstraint tests that one value is less than another.

            //Modifiers
            //...Using(IComparer comparer)
            //...Using<T>(IComparer<T> comparer)
            //...Using<T>(Comparison<T> comparer)

            Assert.That(result, Is.LessThan(49));
            Assert.That(3, Is.LessThan(7));
            Assert.That(-5, Is.Negative);
            //Assert.That(myOwnObject, Is.LessThan(theExpected).Using(myComparer));
        }

        [Test]
        public void LessThanOrEqualToTest()
        {
            //LessThanOrEqualConstraint tests that one value is less than or equal to another.

            //Modifiers
            //...Using(IComparer comparer)
            //...Using<T>(IComparer<T> comparer)
            //...Using<T>(Comparison<T> comparer)

            Assert.That(result, Is.LessThanOrEqualTo(42));
            Assert.That(3, Is.LessThanOrEqualTo(7));
            Assert.That(3, Is.AtMost(7));
            Assert.That(3, Is.LessThanOrEqualTo(3));
            Assert.That(3, Is.AtMost(3));
            //Assert.That(myOwnObject, Is.LessThanOrEqualTo(theExpected).Using(myComparer));
        }

        [Test]
        public void IsPositiveTest()
        {
            Assert.That(result, Is.Positive);
        }

        [Test]
        public void EqualityTest()
        {
            Assert.That(result, Is.EqualTo(42));
            Assert.That(result, Is.Not.EqualTo(7));
        }

        [Test]
        public void RangeTest()
        {
            //RangeConstraint tests that a value is in an (inclusive) range.

            //Modifiers
            //...Using(IComparer comparer)
            //...Using<T>(IComparer<T> comparer)
            //...Using<T>(Comparison<T> comparer)

            int[] intArray = new int[] { 1, 2, 3 };

            Assert.That(result, Is.InRange(40, 80));
            Assert.That(result, Is.Not.InRange(20, 40));
            Assert.That(result, Is.GreaterThan(4).And.LessThan(50));
            Assert.That(result, Is.LessThan(10).Or.GreaterThan(40));
            Assert.That(42, Is.InRange(1, 100));
            Assert.That(intArray, Is.All.InRange(1, 3));
            //Assert.That(myOwnObject, Is.InRange(lowExpected, highExpected).Using(myComparer));
        }

        [Test]
        public void ReferenceEquality()
        {
            var a = new List<string> { "a", "b" };
            var b = a;

            Assert.That(b, Is.SameAs(a));

            var x = new List<string> { "a", "b" };
            var y = new List<string> { "a", "b" };

            Assert.That(y, Is.Not.SameAs(x));
        }
    }
}
