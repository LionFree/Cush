using Cush.Testing;
using NUnit.Framework;

namespace Cush.WPF.Tests
{
    [TestFixture]
    public class BindableBaseTests
    {
        internal class MockClass : BindableBase
        {
            private string _string1;
            private string _string2;

            internal string TestString1
            {
                get { return _string1; }
                set
                {
                    if (_string1 == value) return;
                    _string1 = value;
                    OnPropertyChanged();
                }
            }

            internal string TestString2
            {
                get { return _string2; }
                set { SetProperty(ref _string2, value); }
            }
        }

        [Test]
        public void Given_SetterCallsOnPropertyChanged_When_PropertyIsChanged_Then_Notified()
        {
            var sut = new MockClass();
            sut.ShouldNotifyOn(m => m.TestString1).When(m => m.TestString1 = "newValue");
        }

        [Test]
        public void Given_SetterCallsOnPropertyChanged_When_PropertyIsNotChanged_Then_NotNotified()
        {
            var sut = new MockClass();
            sut.ShouldNotNotifyOn(m => m.TestString1).When(m => m.TestString1 = m.TestString1);
        }

        [Test]
        public void Given_PropertySetterUsesSetProperty_When_PropertyIsChanged_Then_Notified()
        {
            var sut=new MockClass();
            sut.ShouldNotifyOn(m => m.TestString2).When(m => m.TestString2 = "newValue");
        }

        [Test]
        public void Given_PropertySetterUsesSetProperty_When_PropertyIsNotChanged_Then_NotNotified()
        {
            var sut = new MockClass();
            sut.ShouldNotNotifyOn(m => m.TestString2).When(m => m.TestString2 = m.TestString2);
        }
    }
}