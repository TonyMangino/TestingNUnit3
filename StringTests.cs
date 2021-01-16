using NUnit.Framework;

namespace NUnit3Tests
{
    [Category("String tests")]
    public class StringTests
    {
        private readonly string result = "foobar";
        private readonly string nuttin = string.Empty;

        [Test]
        public void EqualityTest()
        {
            Assert.That(result, Is.EqualTo("foobar"));
            Assert.That(result, Is.Not.EqualTo("foobaz"));
            Assert.That(result, Is.EqualTo("FOOBAR").IgnoreCase);
        }

        [Test]
        public void SubstringTest()
        {
            //SubstringConstraint tests for a substring.

            string phrase = "Make your tests fail before passing!";

            Assert.That(phrase, Does.Contain("tests fail"));
            Assert.That(phrase, Does.Not.Contain("tests pass"));
            Assert.That(phrase, Does.Contain("make").IgnoreCase);

            Assert.That(result, Does.Contain("oba").IgnoreCase);
            Assert.That(result, Does.Not.Contain("lmn").IgnoreCase);
        }

        [Test]
        public void EmptyTest()
        {
            //The EmptyStringConstraint tests if a string is empty.

            //Is.Empty actually creates an EmptyConstraint. Subsequently applying
            //it to a string causes an EmptyStringConstraint to be created.

            Assert.That(nuttin, Is.Empty);
            Assert.That(result, Is.Not.Empty);
        }

        [Test]
        public void StartsWithTest()
        {
            //StartsWithConstraint tests for an initial string.

            //StartsWith may appear only in the body of a constraint 
            //expression or when the inherited syntax is used.

            Assert.That(result, Does.StartWith("foo"));
            Assert.That(result, Does.StartWith("FOO").IgnoreCase);
            Assert.That(result, Does.Not.StartWith("moo"));
            Assert.That(result, Does.Not.StartWith("MOO").IgnoreCase);
        }

        [Test]
        public void EndsWithTest()
        {
            //EndsWithConstraint tests for an ending string.

            //EndsWith may appear only in the body of a constraint 
            //expression or when the inherited syntax is used.

            Assert.That(result, Does.EndWith("bar"));
            Assert.That(result, Does.EndWith("BAR").IgnoreCase);
            Assert.That(result, Does.Not.EndWith("baz"));
        }

        [Test]
        public void RegexTest()
        {
            //RegexConstraint tests that a pattern is matched.

            //Matches may appear only in the body of a constraint 
            //expression or when the inherited syntax is used.

            string phrase = "Make your tests fail before passing!";

            Assert.That(result, Does.Match("f*r"));
            Assert.That(result, Does.Not.Match("m*n"));
            Assert.That(phrase, Does.Match("Make.*tests.*pass"));
            Assert.That(phrase, Does.Match("make.*tests.*PASS").IgnoreCase);
            Assert.That(phrase, Does.Not.Match("your.*passing.*tests"));
        }
    }
}
