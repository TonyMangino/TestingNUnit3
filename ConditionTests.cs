using NUnit.Framework;
using System.Collections.Generic;

namespace NUnit3Tests
{
    [Category("Condition tests")]
    public class ConditionTests
    {
        [Test]
        public void EmptyTest()
        {
            //EmptyConstraint tests that an object is an empty string, directory or collection.

            //A DirectoryInfo argument is required in order to test for an empty directory. 
            //To test whether a string represents a directory path, you must first construct a DirectoryInfo.

            var stringArray = new string[] { };
            var list = new List<string>();

            Assert.That(stringArray, Is.Empty);
            Assert.That(list, Is.Empty);
        }

        [Test]
        public void FalseTest()
        {
            //FalseConstraint tests that a value is false.

            var meaningOfLife = 42 < 0;

            Assert.That(meaningOfLife, Is.False);
        }

        [Test]
        public void TrueTest()
        {
            //TrueConstraint tests that a value is true.

            bool meaningOfLife = 42 > 0;

            Assert.That(meaningOfLife, Is.True);
        }

        [Test]
        public void NaNTest()
        {
            //NaNConstraint tests that a value is floating-point NaN.

            double aDouble = double.NaN;
            var bDouble = 42.2;

            Assert.That(aDouble, Is.NaN);
            Assert.That(bDouble, Is.Not.NaN);
        }

        [Test]
        public void NullTest()
        {
            //NullConstraint tests that a value is null.

            object firstObject = null;
            var secondObject = new object();

            Assert.That(firstObject, Is.Null);
            Assert.That(secondObject, Is.Not.Null);
        }
    }
}
