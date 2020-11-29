using NUnit.Framework;
using System;

namespace NUnit3Tests
{
    [Category("Exception tests")]
    public class ExceptionTests
    {
        private class LoanTerm
        {
            //This class was auto-generated just to allow for removing reference errors.
            private int v;

            public LoanTerm(int v)
            {
                this.v = v;
            }
        }

        private interface IEmployee
        {
            void Age(int years);
            void Level(int payLevel);
            string Name { get; set; }
        }

        private class Employee : IEmployee
        {
            private int age, level;

            public string Name { get; set; }

            public void Age(int years)
            {

                if (years < 66)
                {
                    throw new ArgumentException("Get lost you, whippersnapper!");
                }

                age = years;
            }

            public void Level(int payLevel)
            {
                if (level < 1)
                {
                    throw new ArgumentOutOfRangeException("level", "Level must be greater than zero");
                }

                level = payLevel;
            }
        }

        [Test]
        [Category("Exception type is thrown test")]
        public void ExpectedTypeOfExceptionTest()
        {
            IEmployee emp = new Employee();

            Assert.That(() => emp.Age(0), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        [Category("Exception message test")]
        public void ExceptionMessageComparisonTest()
        {
            IEmployee emp = new Employee();

            Assert.That(() => emp.Age(0), Throws.TypeOf<ArgumentException>()
                             .With
                             .Property("Message")
                             .EqualTo("Get lost you, whippersnapper!"));

            Assert.That(() => emp.Age(0), Throws.TypeOf<ArgumentException>()
                             .With
                             .Message
                             .EqualTo("Get lost you, whippersnapper!"));

            Exception ex = Assert.Throws<ArgumentException>(() => emp.Age(0));

            Assert.That(ex.Message, Is.EqualTo("Get lost you, whippersnapper!"));
        }

        [Test]
        [Category("Exception property test")]
        public void ThrowsException()
        {
            IEmployee emp = new Employee();

            Assert.That(() =>
                emp.Level(0),
                    Throws.TypeOf<ArgumentOutOfRangeException>()
                        .With
                        .Property("ParamName")
                        .EqualTo("level"));

            Assert.That(() =>
                emp.Level(0),
                    Throws.TypeOf<ArgumentOutOfRangeException>()
                        .With
                        .Matches<ArgumentOutOfRangeException>(
                            ex => ex.ParamName == "level"));
        }
    }
}