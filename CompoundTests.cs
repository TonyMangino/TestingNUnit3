using NUnit.Framework;
using System.Collections.Generic;

namespace NUnit3Tests
{
    [Category("Compound tests")]
    public class CompoundTests
    {
        [Test]
        public void AndTest()
        {
            //AndConstraint combines two other constraints and succeeds only if they both succeed.

            //Note that the constraint evaluates the sub-constraints left to right.
            //The OrConstraint has precedence over the AndConstraint.

            Assert.That(2.3, Is.GreaterThan(2.0).And.LessThan(3.0));
        }

        [Test]
        public void NotTest()
        {
            //NotConstraint reverses the effect of another constraint. If the base constraint fails, 
            //NotConstraint succeeds. If the base constraint succeeds, NotConstraint fails.

            int[] array = new int[] { 1, 2, 2, 3, 4, 5 };

            Assert.That(array, Is.Not.Unique);
            Assert.That(2 + 2, Is.Not.EqualTo(5));
        }

        [Test]
        public void OrTest()
        {
            //OrConstraint combines two other constraints and succeeds if either of them succeeds.

            //Note that the constraint evaluates the sub-constraints left to right.
            //The OrConstraint has precedence over the AndConstraint.

            Assert.That(3, Is.LessThan(5).Or.GreaterThan(10));
        }
    }
}
