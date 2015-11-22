using Cush.Common.Interaction;
using Cush.Testing;
using NUnit.Framework;

namespace Cush.Common.Tests.Interaction
{
    [TestFixture]
    internal class NullDialogsTests
    {
        private IDialogs _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = NullDialogs.Default;
        }

        [Test]
        public void Constructor()
        {
            object temp = null;
            Assert.DoesNotThrow(() => { temp = NullDialogs.Default; });
            var actual = temp as NullDialogs;
            Assert.IsNotNull(actual);
        }

        [Test]
        public void ShowError()
        {
            var message = GetRandom.String(5, 20);
            Assert.DoesNotThrow(()=>_sut.ShowError(message));
        }

        [Test]
        public void ShowError_withOwner()
        {
            var message = GetRandom.String(5, 20);
            Assert.DoesNotThrow(() => _sut.ShowError(GetType(), message));
        }

        [Test]
        public void ShowMessage_withIcon()
        {
            var title = GetRandom.String(5, 20);
            var message = GetRandom.String(5, 20);
            var icon = new object();
            Assert.DoesNotThrow(() => _sut.ShowMessage(GetType(), title, message, icon));
        }
    }
}