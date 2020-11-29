using NUnit.Framework;

namespace NUnit3Tests
{
    [Category("String tests")]
    public class StringTests
    {
        private readonly string result = "foobar";
        private readonly string nuttin = string.Empty;

        [Test]
        [Category("Equality test")]
        public void EqualityTest()
        {
            Assert.That(result, Is.EqualTo("foobar"));
            Assert.That(result, Is.Not.EqualTo("foobaz"));
            Assert.That(result, Is.EqualTo("FOOBAR").IgnoreCase);
        }

        [Test]
        [Category("Substring test")]
        public void SubstringTest()
        {
            Assert.That(result, Does.Contain("oba").IgnoreCase);
            Assert.That(result, Does.Not.Contain("lmn").IgnoreCase);
        }

        [Test]
        [Category("Empty test")]
        public void EmptyTest()
        {
            Assert.That(nuttin, Is.Empty);
            Assert.That(result, Is.Not.Empty);
        }

        [Test]
        [Category("Starts with test")]
        public void StartsWithTest()
        {
            Assert.That(result, Does.StartWith("foo"));
            Assert.That(result, Does.Not.StartWith("moo"));
        }

        [Test]
        [Category("Ends with test")]
        public void EndsWithTest()
        {
            Assert.That(result, Does.EndWith("bar"));
            Assert.That(result, Does.Not.EndWith("baz"));
        }

        [Test]
        [Category("Regex test")]
        public void RegexTest()
        {
            Assert.That(result, Does.Match("f*r"));
            Assert.That(result, Does.Not.Match("m*n"));
        }
    }
}
