using NUnit.Framework;
using System.Collections.Generic;

namespace NUnit3Tests
{
    [Category("Comparison tests")]
    public class ComparisonTests
    {
        private readonly int result = 42;
        private bool meaningOfLife = 42 > 0;

        [Test]
        [Category("Boolean test")]
        public void BooleanTest()
        {
            Assert.That(meaningOfLife, Is.True);
        }

        [Test]
        [Category("Equality test")]
        public void EqualityTest()
        {
            Assert.That(result, Is.EqualTo(42));
            Assert.That(result, Is.Not.EqualTo(7));
        }

        [Test]
        [Category("Equality test")]
        public void GreaterThanTest()
        {
            Assert.That(result, Is.GreaterThan(2));
            Assert.That(result, Is.GreaterThanOrEqualTo(42));
        }

        [Test]
        [Category("Equality test")]
        public void LessThanTest()
        {
            Assert.That(result, Is.LessThan(49));
            Assert.That(result, Is.LessThanOrEqualTo(42));
        }

        [Test]
        [Category("Range test")]
        public void RangeTest()
        {
            Assert.That(result, Is.InRange(40, 80));
            Assert.That(result, Is.Not.InRange(20, 40));
            Assert.That(result, Is.GreaterThan(4).And.LessThan(50));
            Assert.That(result, Is.LessThan(10).Or.GreaterThan(40));
        }

        [Test]
        [Category("Reference equality test")]
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
