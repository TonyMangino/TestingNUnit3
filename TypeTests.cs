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
        [Category("Instane of tests")]
        public void InstanceOfTest()
        {
            IEmployee emp = new Employee();

            Assert.That(emp, Is.InstanceOf<IEmployee>());
            Assert.That(emp, Is.Not.InstanceOf<string>());
        }

        [Test]
        [Category("Type of tests")]
        public void TypeOfTest()
        {
            IEmployee emp = new Employee();

            Assert.That(emp, Is.TypeOf<Employee>());
        }

        [Test]
        [Category("Assignable to tests")]
        public void AssignabletoTypeTest()
        {
            IEmployee emp = new Employee();

            Assert.That(emp, Is.AssignableTo<Employee>());
        }
    }
}
