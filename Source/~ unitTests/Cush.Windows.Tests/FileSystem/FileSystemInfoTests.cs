using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using Cush.Testing;
using Cush.Windows.FileSystem;
using NUnit.Framework;

namespace Cush.Windows.Tests.FileSystem
{
    [TestFixture]
    internal class FileSystemInfoTests
    {
        private string _path;
        private FileInfo _fileInfo;
        
        private string _folder;
        private DirectoryInfo _directoryInfo;
        private DirectoryInfo _dummy;
        private FileInfo _dummyFile;
        
        [OneTimeSetUp]
        public void FixtureSetup()
        {
            _path = Assembly.GetCallingAssembly().Location;
            _fileInfo = new FileInfo(_path);
            _folder = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
            Assert.IsNotNull(_folder);
            _directoryInfo = new DirectoryInfo(_folder);
        }

        
        private static FileInfo CreateFile()
        {
            var basePath = Environment.CurrentDirectory;
            var path = basePath + @"\Temp_" + Path.GetRandomFileName();
            var fi1 = new FileInfo(path);
            using (fi1.CreateText())
            {
            }
            Assert.IsTrue(fi1.Exists);
            return fi1;
        }

        public DirectoryInfo CreateDirectory()
        {
            // Using Resharper, folders will be build in 
            // %LocalAppData%\JetBrains\Installations\ReSharperPlatformVs12
            // var basePath = @"%LocalAppData%\JetBrains\Installations\ReSharperPlatformVs12";

            var newFolder = NewRelativeDirectoryName();
            var di1 = new DirectoryInfo(_folder);
            var di = di1.CreateSubdirectory(newFolder);
            Assert.IsNotNull(di);
            Assert.IsTrue(di.Exists);

            return di;
        }

        private static string NewRelativeDirectoryName()
        {
            return @"Temp_" + Path.GetRandomFileName();
        }

        [Test]
        public void Exists_False()
        {
            var path = Path.GetRandomFileName();
            var expected = new DirectoryInfo(path).Exists;
            var sut = new DirectoryInfoProxy(path);
            var actual = sut.Exists;
            Assert.AreEqual(expected, actual, "Property does not match wrapped property.");
        }

        [Test]
        public void Exists_True()
        {
            var sut = new DirectoryInfoProxy(_folder);
            var actual = sut.Exists;
            Assert.IsTrue(actual, "Property does not match wrapped property.");
        }

        [Test]
        public void Name()
        {
            var expected = _directoryInfo.Name;
            var sut = new DirectoryInfoProxy(_folder);
            var actual = sut.Name;
            Assert.AreEqual(expected, actual, "Property does not match wrapped property.");
        }

        [Test]
        public void Refresh_And_Attributes()
        {
            _dummy = CreateDirectory();
            var fileName = _dummy.FullName;

            var fi = new DirectoryInfoProxy(fileName);
            var f1Attr = fi.Attributes; // Touching the attributes makes them stick/stale.
            Assert.IsNotNull(f1Attr);

            var fi2 = new DirectoryInfoProxy(fileName);
            fi2.Attributes = fi2.Attributes | FileAttributes.ReadOnly;

            f1Attr = fi.Attributes; // ...still stale...
            var f2Attr = fi2.Attributes;

            Assert.AreNotEqual(f1Attr, f2Attr);
            Assert.AreNotEqual(fi.Attributes, fi2.Attributes);

            fi.Refresh();

            Assert.AreEqual(fi.Attributes, fi2.Attributes);
        }

        [Test]
        public void Delete_File_OK()
        {
            _dummyFile = CreateFile();
            var sut = new FileInfoProxy(_dummyFile.Name);

            Assert.DoesNotThrow(() => sut.Delete(), "Method failed, throwing an exception.");
            _dummyFile.Refresh();
            Assert.IsFalse(_dummyFile.Exists);
        }

        [Test]
        public void CreationTime()
        {
            _dummyFile = CreateFile();
            var sut = new FileInfoProxy(_dummyFile.Name);

            var expected = NewRandom.DateTime(sut.CreationTime);


            Assert.AreNotEqual(expected, sut.CreationTime, "Times should not be the same, but are.");
            Assert.DoesNotThrow(() => sut.CreationTime = expected, "Exception setting property.");
            Assert.AreEqual(expected, sut.CreationTime, "Property not set correctly.");
        }

        [Test]
        public void CreationTimeUtc()
        {
            _dummyFile = CreateFile();
            var sut = new FileInfoProxy(_dummyFile.Name);

            var expected = NewRandom.DateTime(sut.CreationTimeUtc);


            Assert.AreNotEqual(expected, sut.CreationTimeUtc, "Times should not be the same, but are.");
            Assert.DoesNotThrow(() => sut.CreationTimeUtc = expected, "Exception setting property.");
            Assert.AreEqual(expected, sut.CreationTimeUtc, "Property not set correctly.");
        }

        [Test]
        public void LastAccessTime()
        {
            _dummyFile = CreateFile();
            var sut = new FileInfoProxy(_dummyFile.Name);

            var expected = NewRandom.DateTime(sut.LastAccessTime);


            Assert.AreNotEqual(expected, sut.LastAccessTime, "Times should not be the same, but are.");
            Assert.DoesNotThrow(() => sut.LastAccessTime = expected, "Exception setting property.");
            Assert.AreEqual(expected, sut.LastAccessTime, "Property not set correctly.");
        }

        [Test]
        public void LastAccessTimeUtc()
        {
            _dummyFile = CreateFile();
            var sut = new FileInfoProxy(_dummyFile.Name);

            var expected = NewRandom.DateTime(sut.LastAccessTimeUtc);


            Assert.AreNotEqual(expected, sut.LastAccessTimeUtc, "Times should not be the same, but are.");
            Assert.DoesNotThrow(() => sut.LastAccessTimeUtc = expected, "Exception setting property.");
            Assert.AreEqual(expected, sut.LastAccessTimeUtc, "Property not set correctly.");
        }

        [Test]
        public void LastWriteTime()
        {
            _dummyFile = CreateFile();
            var sut = new FileInfoProxy(_dummyFile.Name);

            var expected = NewRandom.DateTime(sut.LastWriteTime);


            Assert.AreNotEqual(expected, sut.LastWriteTime, "Times should not be the same, but are.");
            Assert.DoesNotThrow(() => sut.LastWriteTime = expected, "Exception setting property.");
            Assert.AreEqual(expected, sut.LastWriteTime, "Property not set correctly.");
        }

        [Test]
        public void LastWriteTimeUtc()
        {
            _dummyFile = CreateFile();
            var sut = new FileInfoProxy(_dummyFile.Name);

            var expected = NewRandom.DateTime(sut.LastWriteTimeUtc);


            Assert.AreNotEqual(expected, sut.LastWriteTimeUtc, "Times should not be the same, but are.");
            Assert.DoesNotThrow(() => sut.LastWriteTimeUtc = expected, "Exception setting property.");
            Assert.AreEqual(expected, sut.LastWriteTimeUtc, "Property not set correctly.");
        }


        [Test]
        public void FullName()
        {
            var expected = _fileInfo.FullName;
            var sut = new FileInfoProxy(_path);
            var actual = sut.FullName;

            Assert.AreEqual(expected, actual, "Property does not match wrapped property.");
        }

        [Test]
        public void Test_Equality()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            var differentFolder = Environment.CurrentDirectory;

            var item1 = new DirectoryInfoProxy(folder);
            var item2 = new DirectoryInfoProxy(folder);
            var item3 = new DirectoryInfoProxy(folder);


            Testing.Cush.TestEquality(item1, item2, item3);

            // set them different by one item property.
            //==========================================
            item1 = new DirectoryInfoProxy(differentFolder);
            Assert.IsTrue(item2.Equals(item3));
            Assert.IsFalse(item1.Equals(item2));
            Assert.IsFalse(item1.Equals(item3));
            Testing.Cush.TestEquality(item1, item2, item3);

            item2 = new DirectoryInfoProxy(differentFolder);
            Assert.IsTrue(item1.Equals(item2));
            Assert.IsFalse(item3.Equals(item1));
            Assert.IsFalse(item3.Equals(item2));
            Testing.Cush.TestEquality(item1, item2, item3);

            // Set them equal.
            item3 = new DirectoryInfoProxy(differentFolder);
            Assert.IsTrue(item1.Equals(item2));
            Assert.IsTrue(item1.Equals(item3));
            Assert.IsTrue(item2.Equals(item3));
            Testing.Cush.TestEquality(item1, item2, item3);
        }

        [Test]
        [SuppressMessage("ReSharper", "EqualExpressionComparison")]
        public void Test_Equality_ReferenceEquals()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            var item1 = new DirectoryInfoProxy(folder);

            Assert.IsFalse(item1.Equals(null));
            Assert.IsTrue(item1.Equals(item1));
        }

        [Test]
        public void Test_GetHashCode()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.Windows);

            // If two distinct objects compare as equal, their hashcodes must be equal.
            var item1 = new DirectoryInfoProxy(folder);
            var item2 = new DirectoryInfoProxy(folder);
            Assert.That(item1 == item2);

            Testing.Cush.TestGetHashCode(item1, item2);
        }

        [Test]
        public void Test_Equality_Operators()
        {
            var folder1 = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            var folder2 = Environment.GetFolderPath(Environment.SpecialFolder.System);
            
            var item1 = new DirectoryInfoProxy(folder1);
            var item3 = item1;
            var item2 = new DirectoryInfoProxy(folder2);
            DirectoryInfoProxy item4 = null;
            
            Assert.IsTrue(item1 == item3);
            Assert.IsTrue(item1 != item2);
            Assert.IsTrue(item1 != item4);
        }
    }
}