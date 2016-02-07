using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Cush.Common.Annotations;
using Cush.Common.Helpers;
// ReSharper disable UnusedMember.Global

namespace Cush.Common.FileHandling
{
    [DataContract, Serializable, XmlType("MRUEntry"), DebuggerStepThrough]
    [SettingsSerializeAs(SettingsSerializeAs.Xml)]
    public sealed class MRUEntry : INotifyPropertyChanged
    {
        [field: NonSerialized]
        private static readonly MRUEntryHelper Helper = MRUEntryHelper.GetInstance();

        #region Constructors

        // public default constructor
        public MRUEntry() : this(string.Empty, false, string.Empty)
        {
        }

        public MRUEntry(string fullPath, bool pinned, string tag)
        {
            FullPath = fullPath;
            Pinned = pinned;
            Tag = tag;
        }

        #endregion

        #region Configuration Properties

        [field: NonSerialized]
        private const string PathProperty = "fullPath";

        [field: NonSerialized]
        private const string PinnedProperty = "pinned";

        [field: NonSerialized]
        private const string TagProperty = "tag";

        /// <summary>
        ///     The full path of the file.  Includes the path, filename, and extension.
        /// </summary>
        [XmlAttribute, DataMember, ConfigurationProperty(PathProperty, IsRequired = true)]
        public string FullPath
        {
            get { return (string) _fullPath; }
            set
            {
                if (_fullPath == value) return;
                _fullPath = value;
                OnPropertyChanged(nameof(FullPath));
            }
        }

        /// <summary>
        ///     The state of the entry; is it pinned to the top of the list?
        /// </summary>
        [XmlAttribute, DataMember, ConfigurationProperty(PinnedProperty, IsRequired = false, DefaultValue = false)]
        public bool Pinned
        {
            get { return (bool) _pinned; }
            set
            {
                if (_pinned == value) return;
                _pinned = value;
                OnPropertyChanged(nameof(Pinned));
            }
        }

        /// <summary>
        ///     Tag property.
        /// </summary>
        [XmlAttribute, DataMember, ConfigurationProperty(TagProperty, IsRequired = true)]
        public string Tag
        {
            get { return (string) _tag; }
            set
            {
                if (_tag == value) return;
                _tag = value;
                OnPropertyChanged(nameof(Tag));
            }
        }

        #endregion

        #region Unserialized Helper Properties

        [field: NonSerialized]
        private string[] _location = new string[0];

        [field: NonSerialized]
        private string _shortPath = "";

        private string _fullPath;
        private bool _pinned;
        private string _tag;

        /// <summary>
        ///     The filename (without the path or extension).
        /// </summary>
        [XmlIgnore]
        public string FileName
        {
            get
            {
                // return stripped fullPath
                var filename = Helper.RemoveFileExtension(Helper.GetFilenameWithExtension(FullPath));
                return filename ?? string.Empty;
            }
        }

        /// <summary>
        ///     The filename and extension (without the path).
        /// </summary>
        [XmlIgnore]
        public string FullFileName => Helper.GetFilenameWithExtension(FullPath) ?? string.Empty;

        /// <summary>
        ///     The location/folder where the file exists. (without the filename)
        /// </summary>
        [XmlIgnore]
        public string Location => Helper.GetPathOnly(FullPath) ?? string.Empty;

        [XmlIgnore]
        public string ShortPath
        {
            get { return _shortPath; }
            set
            {
                if (_shortPath == value) return;
                _shortPath = value;
                OnPropertyChanged(nameof(ShortPath));
            }
        }

        /// <summary>
        ///     A graphical element used as a navigational aid in user interfaces.
        /// </summary>
        [XmlIgnore]
        public string BreadCrumbs
        {
            get
            {
                var temp = "";

                if (FullPath != null)
                {
                    _location = Helper.ParseLocation(Location);

                    if (_location.GetLength(0) != 0)
                    {
                        foreach (var item in _location)
                        {
                            // Add the symbol.
                            if (temp != "")
                            {
                                temp += " » ";
                            }
                            temp += item;
                        }
                    }
                }
                return temp;
            }
        }

        public bool IsValid => !string.IsNullOrEmpty(FullPath) && File.Exists(FullPath);

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{{{0}, Pinned={1}}}", FullPath, Pinned);
        }

        #endregion

        #region BindableBase

        /// <summary>
        ///     Multicast event for property change notifications.
        ///     Occurs when a property value changes.
        /// </summary>
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">
        ///     Name of the property used to notify listeners.  This
        ///     value is optional and can be provided automatically when invoked from compilers
        ///     that support <see cref="CallerMemberNameAttribute" />.
        /// </param>
        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}