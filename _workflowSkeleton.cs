using NUnit.Framework;

namespace NUnit3Tests
{
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

    public class Skeleton
    {
        [Test]
        [Ignore("Ignored test method for demostration purposes only")]
        [Author("Tony Mangino")]
        public void IsAnIgnoredTest()
        {
            Assert.That("foo", Is.Not.EqualTo("bar"));
        }
    }
}
