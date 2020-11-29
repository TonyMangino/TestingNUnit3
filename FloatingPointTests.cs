using NUnit.Framework;

namespace NUnit3Tests
{
    [Category("Floating point tests")]
    public class FloatingPointTests
    {
        [Test]
        [Category("Tolerance tests")]
        public void IsWithin()
        {
            double a = 1.0 / 3.0;

            Assert.That(a, Is.EqualTo(0.33).Within(0.004));
            Assert.That(a, Is.EqualTo(0.33).Within(10).Percent);
        }
    }
}
