using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;

namespace Cush.Common.Logging.Internal
{
    [DataContract, KnownType(typeof (Implementation))]
    internal abstract class FileNameFormattingOption
    {
        ///// <summary>
        /////     The name of the assembly.
        ///// </summary>
        //internal static readonly FileNameFormattingOption AssemblyName
        //    = new Implementation("AssemblyName", string.Empty, () => AssemblyInspector.Default.HasEntryAssembly
        //        ? AssemblyWrapper.Default.GetEntryAssembly().GetName().Name
        //        : string.Empty);

        /// <summary>
        ///     The date, formatted in the sortable date pattern:
        ///     6/15/2009 1:45:30 PM => 2009-06-15
        /// </summary>
        internal static readonly FileNameFormattingOption SortableDate
            = new Implementation(Strings.SortableDate, string.Format(Strings.SortableDateFormat, DateTime.Today), null);

        /// <summary>
        ///     The hours and minutes offset from UTC:
        ///     6/15/2009 1:45:30 PM -07:00 => -07.00
        /// </summary>
        internal static readonly FileNameFormattingOption UTCOffset
            = new Implementation(Strings.UTCOffset,
                string.Format(Strings.UTCOffsetFormat, DateTime.Today).Replace('/', '.').Replace(':', '.'),
                null);

        internal static ObservableCollection<FileNameFormattingOption> AllOptions
        {
            get
            {
                return new ObservableCollection<FileNameFormattingOption>
                {
                    //AssemblyName,
                    SortableDate,
                    UTCOffset
                };
            }
        }

        internal static ObservableCollection<string> AllOptionNames
        {
            get
            {
                var output = new ObservableCollection<string>();
                foreach (var item in AllOptions)
                {
                    output.Add(item.Name);
                }
                return output;
            }
        }

        internal static FileNameFormattingOption CustomName(string name)
        {
            return new Implementation(Strings.CustomName, name, null);
        }

        private sealed class Implementation : FileNameFormattingOption
        {
            private readonly string _format;
            private readonly Func<string> _function;
            private readonly string _name;

            /// <summary>
            ///     Returns a new instance of the <see cref="FileNameFormattingOption" /> class,
            ///     using the indicated options.
            ///     If <paramref name="function" /> is null, the <paramref name="format" /> string will be used.
            /// </summary>
            /// <param name="name">A string representing the name of the option.</param>
            /// <param name="format">A string representing the value to return.</param>
            /// <param name="function">A <see cref="Func{T}" /> that will produce the <see cref="string" /> value to return.</param>
            internal Implementation(string name, string format, Func<string> function)
            {
                _name = name;
                _format = format;
                _function = function;
            }

            internal override string Name
            {
                get { return _name; }
            }

            internal override string Format
            {
                get { return (_function == null) ? _format : _function.Invoke(); }
            }
        }

        #region Abstract Members

        /// <summary>
        ///     Gets the name of the <see cref="FileNameFormattingOption" />.
        /// </summary>
        [DataMember]
        internal abstract string Name { get; }

        /// <summary>
        ///     Gets the string value of the <see cref="FileNameFormattingOption" />.
        /// </summary>
        [DataMember]
        internal abstract string Format { get; }

        internal static FileNameFormattingOption ByName(string name)
        {
            return AllOptions.FirstOrDefault(item => item.Name == name);
        }

        #endregion
    }
}