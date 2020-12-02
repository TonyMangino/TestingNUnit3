using NUnit.Framework;

namespace NUnit3Tests
{
    [Parallelizable(ParallelScope.Fixtures)]
    public class ParallelTests
    {
        [Test]
        [Category("??")]
        [Parallelizable(ParallelScope.Self)]
        public void SomeTest()
        {
            Assert.That(1, Is.EqualTo(1));
        }
    }
}