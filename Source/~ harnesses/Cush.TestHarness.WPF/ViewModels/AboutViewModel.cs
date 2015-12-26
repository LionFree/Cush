using System;
using System.Reflection;
using Cush.TestHarness.WPF.ViewModels.Interfaces;
using Cush.TestHarness.WPF.Views.Resources;
using Cush.WPF;
//using Strings = Cush.TestHarness.WPF.Views.Resources.Strings;

namespace Cush.TestHarness.WPF.ViewModels
{
    public class AboutViewModel : BindableBase, IAboutViewModel
    {
        private readonly AssemblyCopyrightAttribute _copyright;
        private readonly AssemblyFileVersionAttribute _version;

        public AboutViewModel()
        {
            // Get the application information.
            var app = Assembly.GetExecutingAssembly();
            _copyright = (AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(app, typeof(AssemblyCopyrightAttribute));
            _version = (AssemblyFileVersionAttribute)Attribute.GetCustomAttribute(app, typeof(AssemblyFileVersionAttribute));
        }

        //public string Title => $"{Strings.HEADER_About}{Strings.TEXT_ApplicationName}";

        public string CopyrightString => $"{Views.Resources.Strings.TEXT_ApplicationDescription}{Environment.NewLine}{Views.Resources.Strings.HEADER_CopyrightVersion}{_version.Version}{Environment.NewLine}{_copyright.Copyright}{Environment.NewLine}{Views.Resources.Strings.TEXT_AllRightsReserved}";
    }
}