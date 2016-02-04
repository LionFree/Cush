using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

//#pragma warning disable 1685

namespace Cush.Common
{
    /// <summary>
    ///     Implementation of <see cref="System.ComponentModel.INotifyPropertyChanged" /> to simplify models.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "DelegateSubtraction")]
    [DataContract, Serializable, DebuggerStepThrough]
    public abstract class BindableBase : PropertyChangedBase
    {
    }
}