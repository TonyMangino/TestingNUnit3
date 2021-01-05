using NUnit.Framework;
using System.Collections.ObjectModel;

namespace NUnit3
{
    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    [Category("Collection tests")]
    public class CollectionTests
    {
        private readonly int[] array = new int[] { 1, 2, 3, 4, 5 };
        private readonly int[] arraySubset = new int[] { 2, 3 };
        private readonly string[] stringArray = new string[] { "one", "two", "two", "three" };
        private readonly string[] arrayWithNull = new string[] { "one", "two", null };
        private readonly string[] emptyArray = new string[] { };
        private readonly string[] nullArray;

        [Test]
        [Category("Collection constraint")]
        [Category("Array test")]
        [Category("Null test")]
        public void IsNullTest()
        {
            Assert.That(nullArray, Is.Null);
            Assert.That(stringArray, Is.All.Not.Null);
        }

        [Test]
        [Category("Collection constraint")]
        [Category("Array test")]
        [Category("Contains test")]
        public void ArrayContainsTest()
        {
            Assert.That(arrayWithNull, Is.Not.All.Contains("a"));
            Assert.That(arrayWithNull, Is.Not.All.Contains("a").IgnoreCase);
        }

        [Test]
        [Category("Collection constraint")]
        [Category("Array test")]
        [Category("Equality test")]
        public void AllGreaterThanTest()
        {
            Assert.That(array, Is.All.GreaterThan(0));
            Assert.That(array, Is.All.LessThan(10));
        }

        [Test]
        [Category("Collection constraint")]
        [Category("Array test")]
        [Category("Instance of test")]
        public void InstanceOfTest()
        {
            Assert.That(array, Is.All.InstanceOf<int>());
        }

        [Test]
        [Category("Collection constraint")]
        [Category("Array test")]
        [Category("Empty test")]
        public void EmptyTest()
        {
            Assert.That(emptyArray, Is.Empty);
            Assert.That(array, Is.Not.Empty);
        }

        [Test]
        [Category("Collection constraint")]
        [Category("Array test")]
        [Category("Occurrance test")]
        public void SpecificItemExactlyNTimesTest()
        {
            Assert.That(array, Has.Exactly(1).EqualTo(1));
            Assert.That(array, Has.Exactly(1).EqualTo(1)
                                    .And.Exactly(1).EqualTo(2));
            Assert.That(stringArray, Has.Exactly(1).EqualTo("one")
                                    .And.Exactly(2).EqualTo("two"));
            Assert.That(array, Has.Exactly(5).Items);
        }

        [Test]
        [Category("Collection constraint")]
        [Category("Array test")]
        [Category("Unique test")]
        public void UniqueItemsTest()
        {
            Assert.That(array, Is.Unique);
        }

        [Test]
        [Category("Array test")]
        [Category("Contains test")]
        public void ContainsSpecificItemTest()
        {
            Assert.That(array, Contains.Item(4));
            Assert.That(array, Does.Contain(4));
            Assert.That(array, Does.Not.Contain(42));
        }

        [Test]
        [Category("Collection constraint")]
        [Category("Array test")]
        [Category("Matches test")]
        public void ArrayMatchesTest()
        {
            Assert.That(stringArray, Is.All.Matches<string>(name => name.ToUpperInvariant().Contains("O") || name.ToUpperInvariant().Contains("T")));
            Assert.That(stringArray, Is.All.Matches<string>(name => name.Length >= 3));
            Assert.That(stringArray, Has.Exactly(1).Matches<string>(name => name == "one"));
        }

        [Test]
        [Category("Collection constraint")]
        [Category("Array subset test")]
        public void SubsetTest()
        {
            Assert.That(arraySubset, Is.SubsetOf(array));
        }

        [Test]
        [Category("Collection constraint")]
        [Category("AllItemsConstraint test")]
        public void AllItemsTest()
        {
            //AllItemsConstraint applies a constraint to each item in an IEnumerable, succeeding only if all
            //of them succeed. An exception is thrown if the actual value passed does not implement IEnumerable.

            int[] intArray = new int[] { 1, 2, 3 };
            string[] stringArray = new string[] { "a", "b", "c" };
            Assert.That(intArray, Is.All.Not.Null);
            Assert.That(stringArray, Is.All.InstanceOf<string>());
            Assert.That(intArray, Is.All.GreaterThan(0));
            Assert.That(intArray, Has.All.GreaterThan(0));
        }

        [Test]
        [Category("Collection constraint")]
        [Category("AnyOf test")]
        public void AnyOfTest()
        {
            //AnyOfConstraint is used to determine whether a value is equal to any of the expected values.

            //Modifiers
            //    ...Using(IComparer comparer)
            //    ...Using<T>(IEqualityComparer comparer)
            //    ...Using<T>(Func<T, T, bool>)
            //    ...Using<T>(IComparer<T> comparer)
            //    ...Using<T>(Comparison<T> comparer)
            //    ...Using<T>(IEqualityComparer <T> comparer)

            int[] intArray = new int[] { 0, -1, 42, 100 };

            Assert.That(42, Is.AnyOf(intArray));
        }

        [Test]
        [Category("Collection constraint")]
        [Category("CollectionContains test")]
        public void CollectionContainsTest()
        {
            //CollectionContainsConstraint tests that an IEnumerable contains an object. If  
            //the actual value passed does not implement IEnumerable, an exception is thrown.

            //Modifiers
            //    ...Using(IComparer comparer)
            //    ...Using<T>(IComparer<T> comparer)
            //    ...Using<T>(Comparison<T> comparer)

            int[] intArray = new int[] { 1, 2, 3 };
            string[] stringArray = new string[] { "a", "b", "c" };

            Assert.That(intArray, Has.Member(3));
            Assert.That(stringArray, Has.Member("b"));
            Assert.That(stringArray, Contains.Item("c"));
            Assert.That(stringArray, Has.No.Member("x"));
            Assert.That(intArray, Does.Contain(3));
        }

        [Test]
        [Category("Collection constraint")]
        [Category("CollectionEquivalent test")]
        public void CollectionEquivalentTest()
        {
            //CollectionEquivalentConstraint tests that two IEnumerables are equivalent - that they contain the same 
            //items, in any order. If the actual value passed does not implement IEnumerable an exception is thrown.
            //To compare items in order, use Is.EqualTo().

            int[] intArray = new int[] { 1, 2, 3 };
            string[] stringArray = new string[] { "a", "b", "c" };

            Assert.That(new int[] { 1, 2, 2 }, Is.Not.EquivalentTo(intArray));
            Assert.That(new int[] { 1, 2, 3 }, Is.EqualTo(intArray));
            Assert.That(new string[] { "c", "a", "b" }, Is.EquivalentTo(stringArray));
        }

        [Test]
        [Category("Collection constraint")]
        [Category("CollectionOrdered test")]
        public void CollectionOrderedTest_SimpleOrdering()
        {
            //CollectionOrderedConstraint tests that an IEnumerable is ordered. If the 
            //actual value passed does not implement IEnumerable, an exception is thrown.
            //The constraint supports both simple and property-based ordering (Ordered.By).
            //By default, the order is expected to be ascending.

            //Simple ordering is based on the values of the items themselves. 
            //It is implied when the By modifier is not used.

            //Modifiers
            //  ...Ascending
            //  ...Descending
            //  ...Using(IComparer comparer)
            //  ...Using<T>(IComparer<T> comparer)
            //  ...Using<T>(Comparison<T> comparer)

            int[] intArray = new int[] { 1, 2, 3 };
            Assert.That(intArray, Is.Ordered);

            string[] stringArray = new string[] { "c", "b", "a" };
            Assert.That(stringArray, Is.Ordered.Descending);

        }

        [Test]
        [Category("Collection constraint")]
        [Category("CollectionOrdered test")]
        public void CollectionOrderedTest_PropertyBasedOrdering()
        {
            //Property-based ordering uses one or more properties that are common 
            //to every item in the enumeration. It is used when one or more  
            //instances of the By modifier appears in the ordering expression.

            //Modifiers
            //  ...Then
            //  ...Ascending
            //  ...Descending
            //  ...By(string propertyName)
            //  ...Using(IComparer comparer)
            //  ...Using<T>(IComparer<T> comparer)
            //  ...Using<T>(Comparison<T> comparer)

            string[] stringArray = new string[] { "a", "aa", "aaa" };
            Assert.That(stringArray, Is.Ordered.By("Length"));

            string[] stringArray2 = new string[] { "aaa", "aa", "a" };
            Assert.That(stringArray2, Is.Ordered.Descending.By("Length"));
        }

        [Test]
        [Category("Collection constraint")]
        [Category("CollectionOrdered test")]
        public void CollectionOrderedTest_MultiplePropertyBasedOrdering()
        {
            //An ordering expression may use multiple By modifiers, each  
            //referring to a different property. The following examples   
            //assume a collection of items with properties named A and B.

            //The Then modifier divides the expression into ordering steps. Each step may 
            //optionally contain one Ascending or Descending modifier and one Using modifier.
            //If Then is not used, each new By modifier marks the beginning of a step. The 
            //last example statement is illegal because the first group contains both Ascending 
            //and Descending. Use of Then is recommended for clarity.

            var collection = new Collection<Person>() {
                new Person
                {
                    FirstName = "Bob",
                    LastName = "Jones"
                },
                new Person
                {
                    FirstName = "John",
                    LastName = "Smith"
                }
            };

            Assert.That(collection, Is.Ordered.By("FirstName").Then.By("LastName"));
            Assert.That(collection, Is.Ordered.By("FirstName").Then.By("LastName").Descending);
            Assert.That(collection, Is.Ordered.Ascending.By("FirstName").Then.Descending.By("LastName"));
            Assert.That(collection, Is.Ordered.Ascending.By("FirstName").By("LastName").Descending);
            //Assert.That(collection, Is.Ordered.Ascending.By("FirstName").Descending.By("LastName")); // Illegal!
        }
    }
}
