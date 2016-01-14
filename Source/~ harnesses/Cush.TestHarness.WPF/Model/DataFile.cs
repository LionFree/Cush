using System;
using System.Xml.Serialization;
using Cush.Common;

namespace Cush.TestHarness.WPF.Model
{
    [Serializable, XmlRoot("Cush.TestHarness.WPF.DataFile")]
    public class DataFile : BindableBase
    {
        public static DataFile Default => new DataFile();

        public DataFile()
        {
            Settings = new Settings();
        }

        [NonSerialized]
        private bool? _isFullyValid = true;

        [XmlIgnore]
        public bool? IsFullyValid
        {
            get { return _isFullyValid; }
            set
            {
                if (value != _isFullyValid)
                {
                    _isFullyValid = value;
                    OnPropertyChanged("IsFullyValid");
                }
            }
        }

        [XmlElement("Settings")]
        public Settings Settings;
    }
}