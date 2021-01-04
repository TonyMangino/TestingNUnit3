using NUnit.Framework;

namespace NUnit3
{
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
            int[] intArray = new int[] { 0, -1, 42, 100 };

            Assert.That(42, Is.AnyOf(intArray));
            Assert.That(myOwnObject, Is.AnyOf(myArray).Using(myComparer));
        }
    }
}
