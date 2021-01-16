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
        public void FileOrDirectoryExistsTest()
        {
            //FileOrDirectoryExistsConstraint tests that a File or Directory exists.

            Assert.That(fileReal, Does.Exist);
            Assert.That(directoryReal, Does.Exist);
            Assert.That(fileFake, Does.Not.Exist);
            Assert.That(directoryFake, Does.Not.Exist);

            Assert.That(new FileInfo(fileReal), Does.Exist);
            Assert.That(new FileInfo(fileFake), Does.Not.Exist);
            Assert.That(new DirectoryInfo(directoryReal), Does.Exist);
            Assert.That(new DirectoryInfo(directoryFake), Does.Not.Exist);
        }

        [Test]
        public void SamePathTest()
        {
            //SamePathConstraint tests that two paths are equivalent.

            //Path constraints perform tests on paths, without reference to any actual files 
            //or directories. This allows testing paths that are created by an application for 
            //reference or later use, without any effect on the environment.

            //Path constraints are intended to work across multiple file systems, 
            //and convert paths to a canonical form before comparing them.

            //It is usually not necessary to know the file system of the paths in order to 
            //compare them. Where necessary, the programmer may use the IgnoreCase and RespectCase 
            //modifiers to provide behavior other than the system default.

            Assert.That("/folder1/./junk/../folder2", Is.SamePath("/folder1/folder2"));
            Assert.That("/folder1/./junk/../folder2/x", Is.Not.SamePath("/folder1/folder2"));

            Assert.That(@"C:\folder1\folder2", Is.SamePath(@"C:\Folder1\Folder2").IgnoreCase);
            Assert.That("/folder1/folder2", Is.Not.SamePath("/Folder1/Folder2").RespectCase);
        }

        [Test]
        public void SamePathOrUnderTest()
        {
            //SamePathOrUnderConstraint tests that one path is equivalent to another path or that it is under it.

            //Path constraints perform tests on paths, without reference to any actual files 
            //or directories. This allows testing paths that are created by an application 
            //for reference or later use, without any effect on the environment.

            //Path constraints are intended to work across multiple file systems, 
            //and convert paths to a canonical form before comparing them.

            //It is usually not necessary to know the file system of the paths in order to 
            //compare them. Where necessary, the programmer may use the IgnoreCase and RespectCase 
            //modifiers to provide behavior other than the system default.

            Assert.That("/folder1/./junk/../folder2", Is.SamePathOrUnder("/folder1/folder2"));
            Assert.That("/folder1/junk/../folder2/./folder3", Is.SamePathOrUnder("/folder1/folder2"));
            Assert.That("/folder1/junk/folder2/folder3", Is.Not.SamePathOrUnder("/folder1/folder2"));

            Assert.That(@"C:\folder1\folder2\folder3", Is.SamePathOrUnder(@"C:\Folder1\Folder2").IgnoreCase);
            Assert.That("/folder1/folder2/folder3", Is.Not.SamePathOrUnder("/Folder1/Folder2").RespectCase);
        }

        [Test]
        public void SubPathTest()
        {
            //SubPathConstraint tests that one path is under another path.

            //Path constraints perform tests on paths, without reference to any actual files 
            //or directories. This allows testing paths that are created by an application 
            //for reference or later use, without any effect on the environment.

            //Path constraints are intended to work across multiple file systems, 
            //and convert paths to a canonical form before comparing them.

            //It is usually not necessary to know the file system of the paths in order to 
            //compare them. Where necessary, the programmer may use the IgnoreCase and RespectCase 
            //modifiers to provide behavior other than the system default.

            //NOTE: The examples from the NUnit website used SubPath(), which produced errors.

            Assert.That("/folder1/folder2/foo", Is.SubPathOf("/folder1/folder2"));
            Assert.That("/folder1/junk/folder2", Is.Not.SubPathOf("/folder1/folder2"));

            Assert.That(@"C:\folder1\folder2\folder3", Is.SubPathOf(@"C:\Folder1\Folder2").IgnoreCase);
            Assert.That("/folder1/folder2/folder3", Is.Not.SubPathOf("/Folder1/Folder2/Folder3").RespectCase);
        }
    }
}
