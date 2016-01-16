using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Cush.Common.ResourceSystem;
using Cush.Windows.FileSystem;
using NUnit.Framework;

namespace Cush.Windows.Tests.FileSystem
{
    [TestFixture]
    internal class DirectoryInfoTests
    {

        internal static class TestHelper
        {
            internal static FileInfo[] GetFileInfos(string path)
            {
                return new DirectoryInfo(path).GetFiles();
            }


            internal static void Refresh(IEnumerable<DirectoryInfo> di)
            {
                foreach (var item in di)
                {
                    item.Refresh();
                }
            }

            internal static bool ShouldExist(bool expected, IEnumerable<DirectoryInfo> di)
            {
                foreach (var item in di)
                {
                    Assert.AreEqual(expected, item.Exists);
                }
                return true;
            }

            internal static DirectoryInfo[] CreateStackOf3Directories()
            {
                var outer = new DirectoryInfo(Environment.CurrentDirectory);
                var d1 = outer.CreateSubdirectory(Path.GetRandomFileName());
                var d2 = d1.CreateSubdirectory(Path.GetRandomFileName());
                var d3 = d2.CreateSubdirectory(Path.GetRandomFileName());

                return new[] { d1, d2, d3 };
            }

            internal static void CheckDirectoryProperties(FileSystemInfo expected, IResourceSystemInfo actual)
            {
                Assert.IsNotNull(expected);
                Assert.AreEqual(expected.FullName, actual.FullName, "Property does not match wrapped property.");
            }

            internal static string NewAbsoluteDirectoryName()
            {
                var basePath = Environment.CurrentDirectory;
                return basePath + @"\" + NewRelativeDirectoryName();
            }

            internal static string NewRelativeDirectoryName()
            {
                return @"Temp_" + Path.GetRandomFileName();
            }


        }

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

        private string _folder;
        private DirectoryInfo _directoryInfo;
        private DirectoryInfo _dummy;

        
        [OneTimeSetUp]
        public void FixtureSetup()
        {
            _folder = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
            Assert.IsNotNull(_folder);
            _directoryInfo = new DirectoryInfo(_folder);
        }

        [Test]
        public void Constructor_EmptyPath()
        {
            object temp = null;
            Assert.Throws<ArgumentException>(() => { temp = new DirectoryInfoProxy(string.Empty); });
            Assert.IsNull(temp, "Exception thrown in constructor, but object constructed.");
        }

        [Test]
        public void Constructor_NullPath()
        {
            object temp = null;
            Assert.Throws<ArgumentNullException>(() => { temp = new DirectoryInfoProxy(path: null); });
            Assert.IsNull(temp, "Exception thrown in constructor, but object constructed.");
        }

        [Test]
        public void Constructor_OK()
        {
            object temp = null;
            Assert.DoesNotThrow(() => { temp = new DirectoryInfoProxy(_folder); });
            var sut = temp as DirectoryInfoProxy;
            Assert.IsNotNull(sut, "Object constructed, but null.");
        }

        [Test]
        public void CreateSublocation_Relative()
        {
            // Using Resharper, folders will be build in 
            // %LocalAppData%\JetBrains\Installations\ReSharperPlatformVs12
            // var basePath = @"%LocalAppData%\JetBrains\Installations\ReSharperPlatformVs12";

            var newFolder = TestHelper.NewRelativeDirectoryName();
            var sut = new DirectoryInfoProxy(_folder);

            DirectoryInfoProxy actual = null;
            Assert.DoesNotThrow(() => actual = sut.CreateSublocation(newFolder));
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Exists);

            DirectoryInfo di = null;
            Assert.DoesNotThrow(() => di = new DirectoryInfo(actual.FullName));
            Assert.IsNotNull(di);
            Assert.IsTrue(di.Exists);

            di.Delete();
        }

        [Test]
        public void ILocationInfo_CreateSublocation_Relative()
        {
            // Using Resharper, folders will be build in 
            // %LocalAppData%\JetBrains\Installations\ReSharperPlatformVs12
            // var basePath = @"%LocalAppData%\JetBrains\Installations\ReSharperPlatformVs12";

            var newFolder = TestHelper.NewRelativeDirectoryName();
            var sut = new DirectoryInfoProxy(_folder);

            ILocationInfo actual = null;
            Assert.DoesNotThrow(() => actual = ((ILocationInfo) sut).CreateSublocation(newFolder));
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Exists);

            DirectoryInfo di = null;
            Assert.DoesNotThrow(() => di = new DirectoryInfo(actual.FullName));
            Assert.IsNotNull(di);
            Assert.IsTrue(di.Exists);

            di.Delete();
        }


        [Test]
        public void CreateSublocation_Rooted()
        {
            var newFolder = TestHelper.NewAbsoluteDirectoryName();

            DirectoryInfoProxy actual = null;
            var sut = new DirectoryInfoProxy(_folder);

            Assert.Throws<ArgumentException>(() => actual = sut.CreateSublocation(newFolder));
            Assert.IsNull(actual);

            var di = new DirectoryInfo(newFolder);
            Assert.IsFalse(di.Exists);
        }

        [Test]
        public void ILocationInfo_Parent()
        {
            var expected = _directoryInfo.Parent;
            var sut = new DirectoryInfoProxy(_folder);
            var actual = ((ILocationInfo) sut).Parent;
            TestHelper.CheckDirectoryProperties(expected, actual);
        }

        [Test]
        public void ILocationInfo_Root()
        {
            var expected = _directoryInfo.Root;
            var sut = new DirectoryInfoProxy(_folder);
            var actual = ((ILocationInfo) sut).Root;
            TestHelper.CheckDirectoryProperties(expected, actual);
        }

        [Test]
        public void Parent()
        {
            var expected = _directoryInfo.Parent;
            var sut = new DirectoryInfoProxy(_folder);
            var actual = sut.Parent;
            TestHelper.CheckDirectoryProperties(expected, actual);
        }

        [Test]
        public void Root()
        {
            var expected = _directoryInfo.Root;
            var sut = new DirectoryInfoProxy(_folder);
            var actual = sut.Root;
            TestHelper.CheckDirectoryProperties(expected, actual);
        }

        [Test]
        public void Delete_Recursive()
        {
            var di = TestHelper.CreateStackOf3Directories();

            Assert.That(TestHelper.ShouldExist(true, di));
            
            var sut = new DirectoryInfoProxy(di[0].FullName);

            
            Assert.DoesNotThrow(() => sut.Delete(true));
            TestHelper.Refresh(di);

            Assert.That(TestHelper.ShouldExist(false, di));

        }

        [Test]
        public void GetLocations_WithoutSearchPattern()
        {
            var di = TestHelper.CreateStackOf3Directories();
            var expected = di[0].GetDirectories();

            var sut = new DirectoryInfoProxy(di[0].FullName);
            var actual = sut.GetLocations();

            foreach (var eItem in expected)
            {
                var found = false;
                foreach (var aItem in actual)
                {
                    if (eItem.FullName == aItem.FullName)
                    {
                        found = true;
                    }
                }
                Assert.IsTrue(found);
            }
            

            di[0].Delete(true);
        }

        [Test]
        public void GetLocations_With_FullName_SearchPattern()
        {
            var di = TestHelper.CreateStackOf3Directories();
            
            var sut = new DirectoryInfoProxy(di[0].FullName);
            
            var actual = sut.GetLocations(di[1].FullName);

            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual(di[1].FullName, actual[0].FullName);
            

            di[0].Delete(true);
        }

        [Test]
        public void GetLocations_With_partial_SearchPattern()
        {
            var di = TestHelper.CreateStackOf3Directories();
            var partialName = di[1].Name;

            var sut = new DirectoryInfoProxy(di[0].FullName);

            var actual = sut.GetLocations(partialName);

            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual(di[1].FullName, actual[0].FullName);


            di[0].Delete(true);
        }

        [Test]
        public void GetLocations_With_null_SearchPattern()
        {
            var di = TestHelper.CreateStackOf3Directories();
            
            var sut = new DirectoryInfoProxy(di[0].FullName);

            var actual = sut.GetLocations(searchPattern: null);

            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Length);

            Assert.AreEqual(new ILocationInfo[0], actual);
            
            di[0].Delete(true);
        }

        [Test]
        public void GetLocations_With_DeepFullName_SearchPattern()
        {
            var di = TestHelper.CreateStackOf3Directories();

            var sut = new DirectoryInfoProxy(di[0].FullName);

            var actual = sut.GetLocations(di[2].FullName);

            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual(di[2].FullName, actual[0].FullName);


            di[0].Delete(true);
        }

        [Test]
        public void GetLocations_With_Empty_SearchPattern()
        {
            var di = TestHelper.CreateStackOf3Directories();

            var sut = new DirectoryInfoProxy(di[0].FullName);

            var actual = sut.GetLocations(string.Empty);

            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Length);
            Assert.AreEqual(new ILocationInfo[0], actual);
            
            di[0].Delete(true);
        }

        [Test]
        public void ILocationInfo_GetLocations()
        {
            var di = TestHelper.CreateStackOf3Directories();
            var partialName = di[1].Name;

            var sut = new DirectoryInfoProxy(di[0].FullName);

            var actual = ((ILocationInfo)sut).GetLocations(partialName);

            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual(di[1].FullName, actual[0].FullName);
            
            di[0].Delete(true);
        }

        [Test]
        public void IResourceInfo_GetResources()
        {
            var location = Environment.CurrentDirectory;
            var expected = TestHelper.GetFileInfos(location);
            var sut = new DirectoryInfoProxy(location);
            var actual = ((ILocationInfo)sut).GetResources();
            
            foreach (var eItem in expected)
            {
                var found = false;
                foreach (var aItem in actual)
                {
                    if (eItem.FullName == aItem.FullName)
                    {
                        found = true;
                    }
                }
                Assert.IsTrue(found);
            }
        }

        

        [Test]
        public void GetResources_WithoutSearchPattern()
        {
            var location = Environment.CurrentDirectory;
            var expected = TestHelper.GetFileInfos(location);
            var sut = new DirectoryInfoProxy(location);
            
            var actual = sut.GetResources();

            foreach (var eItem in expected)
            {
                var found = false;
                foreach (var aItem in actual)
                {
                    if (eItem.FullName == aItem.FullName)
                    {
                        found = true;
                    }
                }
                Assert.IsTrue(found);
            }
        }

        [Test]
        public void GetResources_With_Empty_SearchPattern()
        {
            var sut = new DirectoryInfoProxy(Environment.CurrentDirectory);
            var actual = sut.GetResources(string.Empty);

            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Length);
            Assert.AreEqual(new IResourceInfo[0], actual);
        }

        [Test]
        public void GetResources_With_null_SearchPattern()
        {
            var sut = new DirectoryInfoProxy(Environment.CurrentDirectory);
            var actual = sut.GetResources(null);

            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Length);
            Assert.AreEqual(new IResourceInfo[0], actual);
        }

        [Test]
        public void GetResources_With_FullName_SearchPattern()
        {
            var expected = TestHelper.GetFileInfos(@"C:\");
            Assert.IsNotNull(expected);
            Assert.That(expected.Length > 0);
            var sut = new DirectoryInfoProxy(Environment.CurrentDirectory);
            

            var actual = sut.GetResources(expected[0].FullName);

            Assert.IsNotNull(actual);
            Assert.That(actual.Length > 0);
            Assert.AreEqual(expected[0].FullName, actual[0].FullName);
        }

        

        [Test]
        public void IEquatable_Equality()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            var item1 = new DirectoryInfoProxy(folder);
        
            Assert.IsFalse(((IEquatable<ILocationInfo>)item1).Equals(null));
            Assert.IsTrue(((IEquatable<ILocationInfo>)item1).Equals(item1));
            
        }


        


    }
}