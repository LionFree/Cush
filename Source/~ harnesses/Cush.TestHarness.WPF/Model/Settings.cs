using System;
using System.Linq.Expressions;
using System.Xml.Serialization;
using Cush.Common;

namespace Cush.TestHarness.WPF.Model
{
    [Serializable, XmlType("Settings")]
    public class Settings : PropertyChangedBase
    {
        private string _password = string.Empty;

        public string EncryptedPassword
        {
            get { return _password; }
            set
            {
                SetNotifyingProperty(ref _password, value, () => EncryptedPassword,
                    new Expression<Func<object>>[] {() => HasPassword});
            }
        }

        [XmlIgnore]
        public bool HasPassword => (!string.IsNullOrEmpty(_password));
    }
}