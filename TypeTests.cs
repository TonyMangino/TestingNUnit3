using NUnit.Framework;

namespace NUnit3Tests
{
    [Category("Type tests")]
    public class TypeTests
    {
        private interface IEmployee
        {
            int Age { get; set; }
            string Name { get; set; }
        }

        private class Employee : IEmployee
        {
            public string Name { get; set; }
            public int Age { get; set; }
        };

        [Test]
        public void InstanceOfTest()
        {
            //InstanceOfTypeConstraint tests that an object is of the type supplied or a derived type.

            IEmployee emp = new Employee();

            Assert.That(emp, Is.InstanceOf<IEmployee>());
            Assert.That(emp, Is.Not.InstanceOf<string>());

            Assert.That("Hello", Is.InstanceOf(typeof(string)));
            Assert.That(5, Is.Not.InstanceOf(typeof(string)));
            Assert.That(5, Is.Not.InstanceOf<string>());
        }

        [Test]
        public void ExactTypeTest()
        {
            IEmployee emp = new Employee();

            Assert.That(emp, Is.TypeOf<Employee>());

            Assert.That("Hello", Is.TypeOf(typeof(string)));
            Assert.That("Hello", Is.Not.TypeOf(typeof(int)));

            Assert.That("World", Is.TypeOf<string>());
        }

        [Test]
        public void AssignabletoTypeTest()
        {
            //AssignableToConstraint tests that one type is assignable to another

            IEmployee emp = new Employee();

            Assert.That(emp, Is.AssignableTo<Employee>());

            Assert.That("Hello", Is.AssignableTo(typeof(object)));
            Assert.That(5, Is.Not.AssignableTo(typeof(string)));
        }

        [Test]
        public void AssignableFromTest()
        {
            //AssignableFromConstraint tests that one type is assignable from another

            Assert.That("Hello", Is.AssignableFrom(typeof(string)));
            Assert.That(5, Is.Not.AssignableFrom(typeof(string)));
        }
    }
}
