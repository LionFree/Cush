using System.ComponentModel;

namespace Cush.Common
{
    public class ExtendedPropertyChangedEventArgs<T> : PropertyChangedEventArgs
    {
        public ExtendedPropertyChangedEventArgs(string propertyName, T oldValue, T newValue)
            : base(propertyName)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        public T OldValue { get; private set; }
        public T NewValue { get; private set; }
    }
}