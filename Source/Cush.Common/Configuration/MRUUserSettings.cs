using System;
using System.Configuration;
using System.Diagnostics;

namespace Cush.Common.Configuration
{
    [CLSCompliant(true), DebuggerDisplay("Count: {MRUEntries?.Count}")]
    public sealed class MRUUserSettings : ApplicationSettingsBase
    {
        private const string MRUListProperty = "MRUEntries";

        [UserScopedSetting]
        [SettingsSerializeAs(SettingsSerializeAs.Binary)]
        [DefaultSettingValue("")]
        public MRUList MRUEntries
        {
            get { return (MRUList) this[MRUListProperty]; }
            set { this[MRUListProperty] = value; }
        }
    }
}