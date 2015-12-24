using System.Diagnostics;
using System.Globalization;
using System.Xml.Serialization;
using Cush.WPF.Controls.Helpers;

namespace Cush.WPF.Controls
{
    [XmlType("MRUList"), DebuggerStepThrough]
    public class MRUEntry : BindableBase
    {
        private static readonly MRUEntryHelper Helper = MRUEntryHelper.GetInstance();

        // public constructor
        public MRUEntry()
        {
            // Set the default blank values.
            _fullPath = "";
            _pinned = false;
        }

        [XmlIgnore]
        public string Location
        {
            get
            {
                var temp = "";

                if (FullPath != null)
                {
                    _location = Helper.ParseLocation(FilePath);

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

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{{{0}, Pinned={1}}}", FullPath, Pinned);
        }

        #region Properties

        private string _fullPath = "";
        private string[] _location = new string[0];
        private bool _pinned;
        private string _shortPath = "";

        /// <summary>
        ///     The full path of the file.  Includes the path, filename, and extension.
        /// </summary>
        [XmlAttribute]
        public string FullPath
        {
            get { return _fullPath; }
            set { _fullPath = value; }
        }

        /// <summary>
        ///     The filename (without the path or extension).
        /// </summary>
        [XmlIgnore]
        public string FileName
        {
            get
            {
                // return stripped fullPath
                var filename = Helper.RemoveFileExtension(Helper.GetFilenameWithExtension(_fullPath));
                return filename ?? string.Empty;
            }
        }

        /// <summary>
        ///     The filename and extension (without the path).
        /// </summary>
        [XmlIgnore]
        public string FullFileName
        {
            get { return Helper.GetFilenameWithExtension(_fullPath) ?? string.Empty; }
        }

        /// <summary>
        ///     The path where the file exists. (without the filename)
        /// </summary>
        [XmlIgnore]
        public string FilePath
        {
            get { return Helper.GetPathOnly(_fullPath) ?? string.Empty; }
        }

        [XmlIgnore]
        public string ShortPath
        {
            get { return _shortPath; }
            internal set
            {
                if (_shortPath == value) return;
                _shortPath = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        ///     The state of the entry; is it pinned to the top of the list?
        /// </summary>
        [XmlAttribute]
        public bool Pinned
        {
            get { return _pinned; }
            set
            {
                if (_pinned == value) return;
                _pinned = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}