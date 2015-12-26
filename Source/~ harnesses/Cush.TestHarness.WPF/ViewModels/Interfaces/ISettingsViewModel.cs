using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Cush.WPF;
using Cush.WPF.ColorSchemes;

namespace Cush.TestHarness.WPF.ViewModels.Interfaces
{
    public interface ISettingsViewModel
    {
        List<ThemeMenuData> Accents { get; set; }
        RelayCommand ApplyCommand { get; }
        RelayCommand CancelCommand { get; }
        RelayCommand ClearFilesCommand { get; }
        bool ConfigFileHasPassword { get; set; }
        bool IsBoldChecked { get; set; }
        bool IsItalicChecked { get; set; }
        bool IsKeepRecentFileListChecked { get; set; }
        ThemeMenuData SelectedAccent { get; set; }
        FontFamily SelectedFontFamily { get; set; }
        double SelectedFontSizeInPixels { get; }
        double SelectedFontSizeInPoints { get; set; }
        FontStyle SelectedFontStyle { get; set; }
        FontWeight SelectedFontWeight { get; set; }
        ThemeMenuData SelectedTheme { get; set; }
        bool ShowTooltips { get; set; }
        bool SimilarActivityHandling { get; set; }
        bool SplashOk { get; set; }
        List<ThemeMenuData> Themes { get; set; }
    }
}