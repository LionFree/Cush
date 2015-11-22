using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Cush.ResourceSystem;
using Cush.Windows.FileSystem;
using NUnit.Framework;

namespace Cush.Windows.Tests.FileSystem
{
    [TestFixture]
    internal class FileInfoTests
    {
        [TearDown]
        public void TearDown()
        {
            if (null == _dummy) return;
            if (!_dummy.Exists) return;

            try
            {
                _dummy.Delete();
                _dummy = null;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        private string _path;
        private FileInfo _fileInfo;
        private FileInfo _dummy;

        [OneTimeSetUp]
        public void FixtureSetup()
        {
            _path = Assembly.GetCallingAssembly().Location;
            _fileInfo = new FileInfo(_path);
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

        [Test]
        public void Constructor_OK()
        {
            object temp = null;
            Assert.DoesNotThrow(() => { temp = new FileInfoProxy(_path); });
            var sut = temp as FileInfoProxy;
            Assert.IsNotNull(sut, "Object constructed, but null.");
        }

        [Test]
        public void Constructor_ShouldThrow_EmptyParameter()
        {
            object temp = null;
            Assert.Throws<ArgumentException>(() => { temp = new FileInfoProxy(string.Empty); });
            Assert.IsNull(temp);
        }

        [Test]
        public void Constructor_ShouldThrow_NullParameter()
        {
            object temp = null;
            Assert.Throws<ArgumentNullException>(() => { temp = new FileInfoProxy(fileName: null); });
            Assert.IsNull(temp);
        }
        
        [Test]
        public void Directory()
        {
            var expected = (ILocationInfo) new DirectoryInfoProxy(Path.GetDirectoryName(_path));
            var sut = new FileInfoProxy(_path);
            var actual = sut.Directory;

            Assert.AreEqual(expected.FullName, actual.FullName, "Property does not match wrapped property.");
        }
       
        [Test]
        public void IDirectory_Directory()
        {
            var expected = (ILocationInfo) new DirectoryInfoProxy(Path.GetDirectoryName(_path));
            var sut = new FileInfoProxy(_path);
            var actual = ((IResourceInfo) sut).Location;

            Assert.AreEqual(expected.FullName, actual.FullName, "Property does not match wrapped property.");
        }

        [Test]
        public void IsResourceLocked_FileDoesNotExist()
        {
            _dummy = new FileInfo(Path.GetRandomFileName());
            Assert.IsFalse(_dummy.Exists);

            var sut = new FileInfoProxy(_dummy.Name);

            Assert.IsFalse(sut.IsResourceLocked);
        }

        [Test]
        public void IsResourceLocked_FileExists()
        {
            _dummy = CreateFile();
            FileStream stream = null;

            var sut = new FileInfoProxy(_dummy.Name);
            var nextFi = new FileInfoProxy(_dummy.Name);

            Assert.IsFalse(nextFi.IsResourceLocked);
            Assert.DoesNotThrow(() => stream = sut.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None));
            Assert.IsTrue(nextFi.IsResourceLocked);

            if (stream != null) stream.Close();
        }

        [Test]
        public void Length()
        {
            var expected = _fileInfo.Length;
            var sut = new FileInfoProxy(_path);
            var actual = sut.Length;

            Assert.AreEqual(expected, actual, "Property does not match wrapped property.");
        }
        
        [Test]
        public void Open()
        {
            _dummy = CreateFile();
            FileStream stream = null;

            var sut = new FileInfoProxy(_dummy.FullName);
            Assert.DoesNotThrow(
                () =>
                {
                    stream = sut.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                });

            var nextFi = new FileInfoProxy(_dummy.FullName);
            Assert.Throws<IOException>(
                () =>
                {
                    stream = nextFi.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                });

            if (null != stream) stream.Close();
        }

        

        [Test]
        public void Test_ToString()
        {
            var expected = _fileInfo.Name;
            var sut = new FileInfoProxy(_path);
            var actual = sut.ToString();

            Assert.AreEqual(expected, actual, "Property does not match wrapped property.");
        }
    }
}