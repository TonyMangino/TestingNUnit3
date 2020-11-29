using NUnit.Framework;
using System.IO;

namespace NUnit3Tests
{
    public class DirectoryFileTests
    {
        private readonly string fileReal = @"d:\bar.txt";
        private readonly string fileFake = @"c:\foo\bar.txt";
        private readonly string directoryReal = @"c:\";
        private readonly string directoryFake = @"c:\foo";
        private readonly string directoryEmpty = @"d:\junk\foo";

        [Test]
        [Category("File test")]
        [Category("Exists test")]
        public void FileExistsTest()
        {
            Assert.That(new FileInfo(fileReal), Does.Exist);
            Assert.That(new FileInfo(fileFake), Does.Not.Exist);
        }

        [Test]
        [Category("Directory test")]
        [Category("Exists test")]
        public void DirectoryExistsTest()
        {
            Assert.That(new DirectoryInfo(directoryReal), Does.Exist);
            Assert.That(new DirectoryInfo(directoryFake), Does.Not.Exist);
        }

        [Test]
        [Category("Directory test")]
        [Category("Empty test")]
        public void EmptyDirectoryTest()
        {
            Assert.That(new DirectoryInfo(directoryEmpty), Is.Empty);
        }

        [Test]
        [Category("File test")]
        [Category("Path test")]
        [Category("Equality test")]
        public void SamePathTest()
        {
            Assert.That(directoryEmpty, Is.SamePath(@"d:\junk\foo").IgnoreCase);
        }
    }
}
