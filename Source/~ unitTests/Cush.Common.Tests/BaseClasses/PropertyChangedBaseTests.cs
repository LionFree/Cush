using System;
using System.ComponentModel;
using Cush.Testing;
using NUnit.Framework;

namespace Cush.Common.Tests
{
    [TestFixture]
    internal class PropertyChangedBaseTests
    {
        private void Test_Parameters(object sender, PropertyChangedEventArgs e, bool newValue)
        {
            var args = e as ExtendedPropertyChangedEventArgs<bool>;
            Assert.IsNotNull(args);
            Assert.AreEqual(!newValue, args.OldValue);
            Assert.AreEqual(newValue, args.NewValue);
        }

        [Test]
        public void GetPropertyNames_ThrowsOnNullArray()
        {
            Assert.Throws<ArgumentNullException>(() => TestDisposableClass.GetPropertyNames(null));
        }

        [Test]
        public void OnPropertyChanged_RaisesEventWithExpression()
        {
            var sut = new TestDisposableClass();
            Assert.IsFalse(sut.ResourcesChanged);
            sut.ShouldNotifyOn(m => m.RaiseWithExpression).When(m => m.RaiseWithExpression = true);
        }

        [Test]
        public void OnPropertyChanged_RaisesEventWithMultipleParameters()
        {
            var sut = new TestDisposableClass();
            sut.ShouldNotifyOn(m => m.ResourcesChanged).When(m => m.RaiseWithMultipleParameters = true);

            sut = new TestDisposableClass();
            sut.ShouldNotifyOn(m => m.RaiseWithMultipleParameters).When(m => m.RaiseWithMultipleParameters = true);
        }

        [Test]
        public void OnPropertyChanged_RaisesEventWithNoParameter()
        {
            var sut = new TestDisposableClass();
            Assert.IsFalse(sut.RaiseWithNoParameter);
            sut.ShouldNotifyOn(m => m.RaiseWithNoParameter).When(m => m.RaiseWithNoParameter = true);
        }

        [Test]
        public void OnPropertyChanged_RaisesEventWithParameterName()
        {
            var sut = new TestDisposableClass();
            Assert.IsFalse(sut.RaiseWithParameterName);
            sut.ShouldNotifyOn(m => m.ResourcesChanged).When(m => m.RaiseWithParameterName = true);
        }

        [Test]
        public void PropertyChanged_EventCanBeHookedAndUnhooked()
        {
            var sut = new TestDisposableClass();

            PropertyChangedEventHandler hookup = (x, y) => Test_Parameters(x, y, sut.SetNotifyingPropertyTester);

            Assert.DoesNotThrow(() => sut.PropertyChanged += hookup);
            Assert.IsTrue(sut.IsSubscribedToNotification(hookup));

            Assert.DoesNotThrow(() => sut.PropertyChanged -= hookup);
            Assert.IsFalse(sut.IsSubscribedToNotification(hookup));
        }

        [Test]
        public void SetNotifyingProperty_CanSetNewValueAndNotify()
        {
            var sut = new TestDisposableClass();
            IDisposable expected = null;
            sut.PropertyChanged += (sender, e) =>
            {
                var args = e as ExtendedPropertyChangedEventArgs<IDisposable>;
                Assert.IsNotNull(args);
            };
            sut.ShouldNotifyOn(m => m.ManagedResource).When(m => m.ManagedResource = expected);
            Assert.AreEqual(expected, sut.ManagedResource, "Property did not change.");
        }

        [Test]
        public void SetNotifyingProperty_CanSetNewValueAndNotifyOnMultiple()
        {
            var hits = 0;
            var sut = new TestDisposableClass();
            sut.PropertyChanged += (sender, e) => { hits++; };

            sut.RaiseWithMultipleExpressions = true;

            Assert.AreEqual(3, hits);
        }

        [Test]
        public void SetNotifyingProperty_DoesNotNotifyWhenGivenTheSameValue()
        {
            var sut = new TestDisposableClass();
            sut.ShouldNotNotifyOn(m => m.SetNotifyingPropertyTester)
                .When(m => m.SetNotifyingPropertyTester = sut.SetNotifyingPropertyTester);
        }

        [Test]
        public void SetNotifyingProperty_NotifiesWhenGivenANewValue()
        {
            var sut = new TestDisposableClass();
            sut.PropertyChanged += (x, y) => Test_Parameters(x, y, sut.SetNotifyingPropertyTester);
            sut.ShouldNotifyOn(m => m.SetNotifyingPropertyTester).When(m => m.SetNotifyingPropertyTester = true);
        }
    }
}