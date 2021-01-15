using NUnit.Framework;
using System.IO;

namespace NUnit3Tests
{
    [Category("File and directory tests")]
    public class DirectoryFileTests
    {
        private readonly string fileReal = @"d:\bar.txt";
        private readonly string fileFake = @"c:\foo\bar.txt";
        private readonly string directoryReal = @"c:\";
        private readonly string directoryFake = @"c:\foo";
        private readonly string directoryEmpty = @"d:\junk\foo";

        [Test]
        public void EmptyDirectoryTest()
        {
            //The EmptyDirectoryConstraint tests if a Directory is empty.

            //Is.Empty actually creates an EmptyConstraint. Subsequently applying
            //it to a DirectoryInfo causes an EmptyDirectoryConstraint to be created.

            Assert.That(new DirectoryInfo(directoryEmpty), Is.Empty);
        }

        [Test]
        public void FileExistsTest()
        {
            Assert.That(new FileInfo(fileReal), Does.Exist);
            Assert.That(new FileInfo(fileFake), Does.Not.Exist);
        }

        [Test]
        public void DirectoryExistsTest()
        {
            Assert.That(new DirectoryInfo(directoryReal), Does.Exist);
            Assert.That(new DirectoryInfo(directoryFake), Does.Not.Exist);
        }

        [Test]
        public void SamePathTest()
        {
            Assert.That(directoryEmpty, Is.SamePath(@"d:\junk\foo").IgnoreCase);
        }
    }
}
