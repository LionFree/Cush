using System;
using System.Runtime.Serialization;

namespace Cush.Common
{
    /// <summary>
    ///     A simple base class that provides an implementation of the
    ///     <see cref="System.IEquatable&lt;Object&gt;" /> pattern on top of the
    ///     <see cref="T:System.ComponentModel.INotifyPropertyChanged" />
    ///     implementation of the
    ///     <see cref="T:Cush.Common.PropertyChangedBase" /> base class.
    /// </summary>
    [DataContract]
    public abstract class EquatableBase : PropertyChangedBase, IEquatable<object>
    {
        public abstract override bool Equals(object obj);
        public abstract override int GetHashCode();


        public static bool operator ==(EquatableBase left, EquatableBase right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (((object) left) == null || ((object) right) == null) return false;
            return left.Equals(right);
        }


        public static bool operator !=(EquatableBase left, EquatableBase right)
        {
            return !(left == right);
        }
    }
}