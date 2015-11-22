using System;
using System.Runtime.Serialization;

namespace Cush.Common
{
    /// <summary cref="object">
    ///     A simple base class that provides an implementation of the
    ///     <see cref="System.IEquatable&lt;Object&gt;" /> pattern on top of the
    ///     <see cref="T:System.IDisposable" /> and
    ///     <see cref="T:System.ComponentModel.INotifyPropertyChanged" /> implementations of the
    ///     <see cref="T:Cush.Common.DisposableBase" /> base class.
    /// </summary>
    [DataContract]
    public abstract class DisposableEquatableBase : DisposableBase, IEquatable<object>
    {
        public abstract override bool Equals(object obj);
        public abstract override int GetHashCode();

        public static bool operator ==(DisposableEquatableBase left, DisposableEquatableBase right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (((object) left) == null || ((object) right) == null) return false;
            return left.Equals(right);
        }

        public static bool operator !=(DisposableEquatableBase left, DisposableEquatableBase right)
        {
            return !(left == right);
        }
    }
}