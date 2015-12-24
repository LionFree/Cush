using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cush.TestHarness.WPF.Views.Dialogs
{
    internal class DialogPack
    {
        internal DialogPack(AboutDialog about, SettingsDialog settings)
        {
            About = about;
            Settings = settings;
        }

        internal SettingsDialog Settings { get; set; }

        internal AboutDialog About { get; set; }
    }
}
