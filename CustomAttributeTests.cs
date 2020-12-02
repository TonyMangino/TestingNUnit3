using NUnit.Framework;
using System;

namespace NUnit3Tests
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class CustomCategoryAttribute : CategoryAttribute
    {
        //An advantage of a custom category attribute is you have the benefit of intellisense
        //versus having to remember individual custom category text which is possibly repeated.
    }

    [CustomCategory]
    public class CustomizationClass1
    {
        [Test]
        [Category("Custom category attribute test")]
        public void CustomTest()
        {
            Assert.That(1, Is.EqualTo(1));
        }
    }

    [CustomCategory]
    public class CustomizationClass2
    {
        [Test]
        [Category("Custom category attribute test")]
        public void CustomTest()
        {
            Assert.That(1, Is.EqualTo(1));
        }
    }
}
