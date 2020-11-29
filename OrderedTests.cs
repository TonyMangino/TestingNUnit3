using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace NUnit3Tests
{
    [Category("Ordered tests")]
    public class OrderedTests
    {
        private readonly int[] array = new int[] { 1, 2, 3, 4, 5 };
        private readonly int[] arrayDesc = new int[] { 5, 4, 3, 2, 1 };

        private class Employee
        {
            public string Name { get; set; }
            public int Age { get; set; }
        };

        private readonly List<Employee> employees = new List<Employee>
        {
            new Employee { Name = "Foo", Age = 32 },
            new Employee { Name = "Baz", Age = 49 },
            new Employee { Name = "Bar", Age = 49 }
        };

        [Test]
        [Category("Ascending tests")]
        public void AscendingTest()
        {
            Assert.That(array, Is.Ordered.Ascending);
            Assert.That(employees, Is.Ordered.Ascending.By("Age"));
        }

        [Test]
        [Category("Descending tests")]
        public void DescendingTest()
        {
            Assert.That(arrayDesc, Is.Ordered.Descending);

            var employeesDesc = employees.OrderByDescending(e => e.Age).ToList();
            Assert.That(employeesDesc, Is.Ordered.Descending.By("Age"));
        }

        [Test]
        [Category("Multiple property order test")]
        public void ByMultiplePropertyTest()
        {
            Assert.That(employees, 
                Is
                .Ordered.Ascending.By("Age")
                .Then.Descending.By("Name"));
        }
    }
}