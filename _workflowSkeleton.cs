using NUnit.Framework;

namespace NUnit3Tests
{
    // A SetUpFixture that provides SetUp and TearDown for the entire assembly.
    [SetUpFixture]
    public class MySetUpClass
    {
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            // Executes once before the test run. (Optional)
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            // Executes once after the test run. (Optional)
        }
    }

    [Ignore("Ignored test class for demostration purposes only")]
    [Author("Tony Mangino")]
    public class IgnoredTestClass
    {
        [Test]
        public void Test()
        {
            Assert.That("foo", Is.Not.EqualTo("bar"));
        }

        [Test]
        public void SecondTest()
        {
            Assert.That("foo", Is.Not.EqualTo("bar"));
        }
    }

    //A class that contains NUnit unit tests. (Required)
    [Author("your name here", "your.name@here.com")]
    public class Skeleton
    {
        [OneTimeSetUp]
        public void ClassInitialization()
        {
            //Executes once for the test class. (Optional)
        }

        [SetUp]
        public void TestInitialization()
        {
            // Runs before each test in the class. (Optional)
        }

        [Test]
        [Ignore("Ignored test method for demostration purposes only")]
        [Category("Skeleton ignored test")]
        [Author("your name here")]
        [Repeat(10)]
        [Retry(3)]
        [Timeout(3000)]
        public void IsAnIgnoredTest()
        {
            Assert.That("foo", Is.Not.EqualTo("bar"));
        }

        [Test]
        [Category("Skeleton actual test")]
        public void SomeTest()
        {
            Assert.That(1, Is.EqualTo(1));
        }

        [TearDown]
        public void TestCleanup()
        {
            // Runs after each test in the class. (Optional)
        }

        [OneTimeTearDown]
        public void ClassCleanup()
        {
            // Runs once after all tests in this class are executed. (Optional)
            // Not guaranteed that it executes instantly after all tests from the class.
        }
    }
}
