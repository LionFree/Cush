﻿/*
 * Copyright 2007-2012 JetBrains s.r.o.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;

namespace Cush.Common.Annotations
{
    /// <summary>
    ///     Indicates that the value of the marked element could never be <c>null</c>
    /// </summary>
    /// <example>
    ///     <code>
    /// [NotNull]
    /// public object Foo()
    /// {
    ///   return null; // Warning: Possible 'null' assignment
    /// } 
    /// </code>
    /// </example>
    [AttributeUsage(
        AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Delegate |
        AttributeTargets.Field)]
    public sealed class NotNullAttribute : Attribute
    {
    }

    
    /// <summary>
    ///     Indicates that the method is contained in a type that implements
    ///     <see cref="System.ComponentModel.INotifyPropertyChanged" /> interface
    ///     and this method is used to notify that some property value changed.
    /// </summary>
    /// <remarks>
    ///     The method should be non-static and conform to one of the supported signatures:
    ///     <list>
    ///         <item>
    ///             <c>NotifyChanged(string)</c>
    ///         </item>
    ///         <item>
    ///             <c>NotifyChanged(params string[])</c>
    ///         </item>
    ///         <item>
    ///             <c>NotifyChanged{T}(Expression{Func{T}})</c>
    ///         </item>
    ///         <item>
    ///             <c>NotifyChanged{T,U}(Expression{Func{T,U}})</c>
    ///         </item>
    ///         <item>
    ///             <c>SetProperty{T}(ref T, T, string)</c>
    ///         </item>
    ///     </list>
    /// </remarks>
    /// <example>
    ///     <code>
    ///  public class Foo : INotifyPropertyChanged
    ///  {
    ///    public event PropertyChangedEventHandler PropertyChanged;
    /// 
    ///    [NotifyPropertyChangedInvocator]
    ///    protected virtual void NotifyChanged(string propertyName)
    ///    {}
    /// 
    ///    private string _name;
    ///    public string Name
    ///    {
    ///      get { return _name; }
    ///      set
    ///      {
    ///        _name = value;
    ///        NotifyChanged("LastName"); // Warning
    ///      }
    ///    }
    ///  }
    ///  </code>
    ///     Examples of generated notifications:
    ///     <list>
    ///         <item>
    ///             <c>NotifyChanged("Property")</c>
    ///         </item>
    ///         <item>
    ///             <c>NotifyChanged(() => Property)</c>
    ///         </item>
    ///         <item>
    ///             <c>NotifyChanged((VM x) => x.Property)</c>
    ///         </item>
    ///         <item>
    ///             <c>SetProperty(ref myField, value, "Property")</c>
    ///         </item>
    ///     </list>
    /// </example>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class NotifyPropertyChangedInvocatorAttribute : Attribute
    {
        public NotifyPropertyChangedInvocatorAttribute()
        {
        }

        public NotifyPropertyChangedInvocatorAttribute(string parameterName)
        {
            ParameterName = parameterName;
        }

        [UsedImplicitly]
        public string ParameterName { get; private set; }
    }
    
    /// <summary>
    ///     Indicates that the marked symbol is used implicitly (e.g. via reflection, in external library),
    ///     so this symbol will not be marked as unused (as well as by other usage inspections)
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class UsedImplicitlyAttribute : Attribute
    {
        [UsedImplicitly]
        public UsedImplicitlyAttribute()
            : this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
        {
        }

        [UsedImplicitly]
        public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
        {
            UseKindFlags = useKindFlags;
            TargetFlags = targetFlags;
        }

        [UsedImplicitly]
        public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags)
            : this(useKindFlags, ImplicitUseTargetFlags.Default)
        {
        }

        [UsedImplicitly]
        public UsedImplicitlyAttribute(ImplicitUseTargetFlags targetFlags)
            : this(ImplicitUseKindFlags.Default, targetFlags)
        {
        }

        [UsedImplicitly]
        public ImplicitUseKindFlags UseKindFlags { get; private set; }

        /// <summary>
        ///     Gets value indicating what is meant to be used
        /// </summary>
        [UsedImplicitly]
        public ImplicitUseTargetFlags TargetFlags { get; private set; }
    }
    
    [Flags]
    public enum ImplicitUseKindFlags
    {
        Default = Access | Assign | InstantiatedWithFixedConstructorSignature,

        /// <summary>
        ///     Only entity marked with attribute considered used
        /// </summary>
        Access = 1,

        /// <summary>
        ///     Indicates implicit assignment to a member
        /// </summary>
        Assign = 2,

        /// <summary>
        ///     Indicates implicit instantiation of a type with fixed constructor signature.
        ///     That means any unused constructor parameters won't be reported as such.
        /// </summary>
        InstantiatedWithFixedConstructorSignature = 4,

        /// <summary>
        ///     Indicates implicit instantiation of a type
        /// </summary>
        InstantiatedNoFixedConstructorSignature = 8
    }

    /// <summary>
    ///     Specify what is considered used implicitly when marked with <see cref="MeansImplicitUseAttribute" /> or
    ///     <see cref="UsedImplicitlyAttribute" />
    /// </summary>
    [Flags]
    public enum ImplicitUseTargetFlags
    {
        Default = Itself,

        Itself = 1,

        /// <summary>
        ///     Members of entity marked with attribute are considered used
        /// </summary>
        Members = 2,

        /// <summary>
        ///     Entity marked with attribute and all its members considered used
        /// </summary>
        WithMembers = Itself | Members
    }

    /// <summary>
    ///     Should be used on attributes and causes ReSharper
    ///     to not mark symbols marked with such attributes as unused (as well as by other usage inspections)
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class MeansImplicitUseAttribute : Attribute
    {
        [UsedImplicitly]
        public MeansImplicitUseAttribute()
            : this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
        {
        }

        [UsedImplicitly]
        public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
        {
            UseKindFlags = useKindFlags;
            TargetFlags = targetFlags;
        }

        [UsedImplicitly]
        public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags)
            : this(useKindFlags, ImplicitUseTargetFlags.Default)
        {
        }

        [UsedImplicitly]
        public MeansImplicitUseAttribute(ImplicitUseTargetFlags targetFlags)
            : this(ImplicitUseKindFlags.Default, targetFlags)
        {
        }

        [UsedImplicitly]
        public ImplicitUseKindFlags UseKindFlags { get; private set; }

        /// <summary>
        ///     Gets value indicating what is meant to be used
        /// </summary>
        [UsedImplicitly]
        public ImplicitUseTargetFlags TargetFlags { get; private set; }
    }
}