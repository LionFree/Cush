using System;
using System.ComponentModel;
using System.Diagnostics;
using Cush.Testing.Assertion;

namespace Cush.Testing
{
    public class NotifyExpectation<T> where T : INotifyPropertyChanged
    {
        private readonly bool _eventExpected;
        private readonly T _owner;
        private readonly string _propertyName;

        [DebuggerStepThrough]
        public NotifyExpectation(T owner, string propertyName, bool eventExpected)
        {
            _owner = owner;
            _propertyName = propertyName;
            _eventExpected = eventExpected;
        }

        [DebuggerStepThrough]
        public void When(Action<T> action)
        {
            var eventWasRaised = false;
            _owner.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == _propertyName)
                {
                    eventWasRaised = true;
                }
            };
            action(_owner);

            Assert.AreEqual(_eventExpected, eventWasRaised, "PropertyChanged on {0}", _propertyName);
        }
    }
}
