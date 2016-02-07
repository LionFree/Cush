using System;
using NUnit.Framework;

// ReSharper disable ObjectCreationAsStatement

namespace Cush.WPF.Tests
{
    [TestFixture]
    internal class RelayCommandTests
    {
        internal class MockClass
        {
            private RelayCommand<object> _command1;
            private RelayCommand<object, object> _command2;

            public RelayCommand<object> SingleParameterCommand
            {
                get { return _command1 ?? (_command1 = new RelayCommand<object>(Delegate1)); }
            }

            public RelayCommand<object, object> DoubleParameterCommand
            {
                get { return _command2 ?? (_command2 = new RelayCommand<object, object>(Delegate2)); }
            }

            public object ParameterValue1 { get; private set; }
            public object ParameterValue2 { get; private set; }

            internal void Delegate1(object o)
            {
                Delegate2(o, null);
            }

            internal void Delegate2(object o, object p)
            {
                ParameterValue1 = o;
                ParameterValue2 = p;
            }
        }

        internal void Delegate1(object o)
        {
        }

        internal void Delegate2(object o, object p)
        {
        }

        internal bool CanChange(object o)
        {
            return false;
        }

        [Test]
        public void Given_CanExecuteDelegate_When_ICallCanExecute_ItReturnsCorrectValue()
        {
            var expected = new Random().Next(0, 2) == 1;

            var sut = new RelayCommand<object>(Delegate1, o => expected);

            Assert.AreEqual(expected, sut.CanExecute(null));
        }

        [Test]
        public void Given_CanExecuteDelegate_When_RaiseCanExecuteCalled_EventFires()
        {
            var eventFired = false;
            var handler = new EventHandler((o, e) =>
            {
                eventFired = true;
            });

            var sut = new RelayCommand<object>(Delegate1, CanChange);
            sut.CanExecuteChanged += handler;

            sut.RaiseCanExecuteChanged();
            
            Assert.IsTrue(eventFired);

            sut.CanExecuteChanged -= handler;
        }

        [Test]
        public void Given_DoubleActionIsNull_When_Constructed_Then_ExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => new RelayCommand<object, object>(null));
        }

        [Test]
        public void Given_DoubleParameterCommand_When_AskedForName_Then_NameIsGiven()
        {
            var expected = "DoubleParameter";

            var sut = new RelayCommand<object>(Delegate1, expected);

            Assert.AreEqual(expected, sut.Name);
        }

        [Test]
        public void Given_DoubleParameterCommand_When_ExecuteIsCalled_Then_DelegateIsFired()
        {
            var expected1 = "pizza";
            var expected2 = 42;

            var sut = new MockClass();
            sut.DoubleParameterCommand.Execute(expected1, expected2);

            Assert.AreEqual(expected1, sut.ParameterValue1);
            Assert.AreEqual(expected2, sut.ParameterValue2);
        }

        [Test]
        public void Given_NoCanExecute_When_AskedForCanExecute_CanExecuteReturnsTrue()
        {
            var sut = new RelayCommand<object>(Delegate1);
            Assert.IsTrue(sut.CanExecute(null));
        }

        [Test]
        public void Given_SingleActionIsNull_When_Constructed_Then_ExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => new RelayCommand<object>(null));
        }

        [Test]
        public void Given_SingleParameterCommand_When_AskedForName_Then_NameIsGiven()
        {
            var expected = "SingleParameter";

            var sut = new RelayCommand<object, object>(Delegate2, expected);

            Assert.AreEqual(expected, sut.Name);
        }

        [Test]
        public void Given_SingleParameterCommand_When_ExecuteIsCalled_Then_DelegateIsFired()
        {
            var expected1 = "robble";

            var sut = new MockClass();
            sut.SingleParameterCommand.Execute(expected1);

            Assert.AreEqual(expected1, sut.ParameterValue1);
        }
    }
}