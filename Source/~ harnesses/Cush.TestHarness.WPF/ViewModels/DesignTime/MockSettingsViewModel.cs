using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Media;
using Cush.TestHarness.WPF.ViewModels.Interfaces;
using Cush.WPF;
using Cush.WPF.ColorSchemes;

namespace Cush.TestHarness.WPF.ViewModels.DesignTime
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class MockSettingsViewModel : ISettingsViewModel
    {
        public List<ThemeMenuData> Accents { get; set; }
        public RelayCommand ApplyCommand { get; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand ClearFilesCommand { get; }
        public bool ConfigFileHasPassword { get; set; }
        public bool IsBoldChecked { get; set; }
        public bool IsItalicChecked { get; set; }
        public bool IsKeepRecentFileListChecked { get; set; }
        public ThemeMenuData SelectedAccent { get; set; }
        public FontFamily SelectedFontFamily { get; set; }
        public double SelectedFontSizeInPixels { get; }
        public double SelectedFontSizeInPoints { get; set; }
        public FontStyle SelectedFontStyle { get; set; }
        public FontWeight SelectedFontWeight { get; set; }
        public ThemeMenuData SelectedTheme { get; set; }
        public bool ShowTooltips { get; set; }
        public bool SimilarActivityHandling { get; set; }
        public bool SplashOk { get; set; }
        public List<ThemeMenuData> Themes { get; set; }
    }
}