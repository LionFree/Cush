using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Cush.TestHarness.WPF.Properties;
using Cush.WPF;
using Cush.WPF.ColorSchemes;

namespace Cush.TestHarness.WPF.ViewModels
{

    public interface ISettingsViewModel
    {
        List<ThemeMenuData> Accents { get; set; }
        RelayCommand ApplyCommand { get; }
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

    public sealed class MockSettingsViewModel : ISettingsViewModel
    {
        public List<ThemeMenuData> Accents { get; set; }
        public RelayCommand ApplyCommand { get; }
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

    internal sealed class SettingsViewModel : BindableBase, ISettingsViewModel
    {
        private const double DeviceIndependentPixelsPerInch = 96.0;
        private const double PointsPerInch = 72.0;
        private bool _boldChecked;
        private double _fontSizeInPoints;
        private bool _isKeepRecentFileListChecked;
        private bool _italicChecked;
        private ThemeMenuData _selectedAccent;


        private FontFamily _selectedFontFamily;
        private FontStyle _selectedFontStyle;
        private FontWeight _selectedFontWeight;
        private ThemeMenuData _selectedTheme;
        private bool _similarActivityHandling;

        public SettingsViewModel()
        {

            // Initialize the theme display
            Accents = ColorSchemeManager.Accents
                                  .Select(
                                      a =>
                                      new ThemeMenuData
                                      {
                                          Name = a.DisplayName,
                                          ColorBrush = a.Resources["AccentColorBrush"] as Brush
                                      })
                                  .ToList();

            Themes = ColorSchemeManager.Themes
                                 .Select(
                                     a =>
                                     new ThemeMenuData
                                     {
                                         Name = a.DisplayName,
                                         BorderColorBrush = a.Resources["BlackColorBrush"] as Brush,
                                         ColorBrush = a.Resources["WhiteColorBrush"] as Brush
                                     })
                                 .ToList();

            RefreshSettings();
        }


        public bool ConfigFileHasPassword { get; set; }


        public List<ThemeMenuData> Themes { get; set; }

        public List<ThemeMenuData> Accents { get; set; }

        public double SelectedFontSizeInPixels => _fontSizeInPoints * (DeviceIndependentPixelsPerInch / PointsPerInch);

        public double SelectedFontSizeInPoints
        {
            get { return _fontSizeInPoints; }
            set
            {
                if (Math.Abs(_fontSizeInPoints - value) <= double.Epsilon) return;
                _fontSizeInPoints = value;

                OnPropertyChanged();
                // ReSharper disable once ExplicitCallerInfoArgument
                OnPropertyChanged(nameof(SelectedFontSizeInPixels));
            }
        }

        public RelayCommand ApplyCommand
        {
            get { return new RelayCommand("ApplyCommand", param => Trace.WriteLine("Apply Clicked.")); }
        }

        public RelayCommand ClearFilesCommand
        {
            get { return new RelayCommand("ClearFilesCommand", param => Trace.WriteLine("Clear Files Clicked.")); }
        }

        public FontFamily SelectedFontFamily
        {
            get { return _selectedFontFamily; }
            set
            {
                if (_selectedFontFamily != null && _selectedFontFamily.Equals(value)) return;
                _selectedFontFamily = value;
                OnPropertyChanged();
            }
        }

        public FontStyle SelectedFontStyle
        {
            get { return _selectedFontStyle; }
            set
            {
                if (_selectedFontStyle.Equals(value)) return;
                _selectedFontStyle = value;
                OnPropertyChanged();
            }
        }

        public FontWeight SelectedFontWeight
        {
            get { return _selectedFontWeight; }
            set
            {
                if (_selectedFontWeight.Equals(value)) return;
                _selectedFontWeight = value;
                OnPropertyChanged();
            }
        }



        public bool IsBoldChecked
        {
            get { return _boldChecked; }
            set
            {
                if (_boldChecked == value) return;
                _boldChecked = value;
                SelectedFontWeight = _boldChecked ? FontWeights.Bold : FontWeights.Normal;
                OnPropertyChanged();
            }
        }

        public bool IsItalicChecked
        {
            get { return _italicChecked; }
            set
            {
                if (_italicChecked == value) return;
                _italicChecked = value;

                SelectedFontStyle = _italicChecked ? FontStyles.Italic : FontStyles.Normal;
                OnPropertyChanged();
            }
        }

        public bool IsKeepRecentFileListChecked
        {
            get { return _isKeepRecentFileListChecked; }
            set
            {
                if (_isKeepRecentFileListChecked == value) return;
                _isKeepRecentFileListChecked = value;
                OnPropertyChanged();
            }
        }

        public bool SimilarActivityHandling
        {
            get { return _similarActivityHandling; }
            set
            {
                if (_similarActivityHandling == value) return;
                _similarActivityHandling = value;
                OnPropertyChanged();
            }
        }

        //public ConfigFile ConfigFile
        //{
        //    get { return _configFile; }
        //}

        public ThemeMenuData SelectedAccent
        {
            get { return _selectedAccent; }
            set
            {
                if (_selectedAccent == value) return;
                _selectedAccent = value;
                OnPropertyChanged();
            }
        }

        public ThemeMenuData SelectedTheme
        {
            get { return _selectedTheme; }
            set
            {
                if (_selectedTheme == value) return;
                _selectedTheme = value;
                OnPropertyChanged();
            }
        }

        public bool SplashOk { get; set; }
        public bool ShowTooltips { get; set; }



        private static void DoClearFiles()
        {
            Trace.WriteLine("DoClearFiles");
        }

        private void RefreshSettings()
        {
            var selectedScheme = ColorSchemeManager.CurrentScheme;
            _selectedTheme = Themes.FirstOrDefault(x => x.Name == selectedScheme.Theme.DisplayName);
            _selectedAccent = Accents.FirstOrDefault(x => x.Name == selectedScheme.Accent.DisplayName);

            _selectedFontFamily = Settings.Default.FontFamily ?? new FontFamily("Arial");
            SelectedFontSizeInPoints = Math.Abs(Settings.Default.FontSize) < double.Epsilon
                                           ? 28
                                           : Settings.Default.FontSize;
            IsBoldChecked = Settings.Default.FontBold;
            IsItalicChecked = Settings.Default.FontItalic;

            IsKeepRecentFileListChecked = Settings.Default.KeepRecentFilesList;
            SimilarActivityHandling = Settings.Default.SimilarActivityHandling;
        }

        private void Commit()
        {
            Settings.Default.ShowTooltips = ShowTooltips;
            Settings.Default.SplashOK = SplashOk;
            Settings.Default.Theme = SelectedTheme.Name;
            Settings.Default.Accent = SelectedAccent.Name;
            Settings.Default.FontFamily = SelectedFontFamily;
            Settings.Default.FontSize = SelectedFontSizeInPoints;
            Settings.Default.FontBold = IsBoldChecked;
            Settings.Default.FontItalic = IsItalicChecked;
            Settings.Default.KeepRecentFilesList = IsKeepRecentFileListChecked;
            Settings.Default.SimilarActivityHandling = SimilarActivityHandling;

            // Save the settings
            Settings.Default.Save();
        }
    }
}
