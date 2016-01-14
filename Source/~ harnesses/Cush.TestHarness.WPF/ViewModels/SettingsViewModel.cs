using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Windows;
using System.Windows.Media;
using Cush.Common;
using Cush.TestHarness.WPF.Properties;
using Cush.TestHarness.WPF.ViewModels.Interfaces;
using Cush.WPF;
using Cush.WPF.ColorSchemes;

namespace Cush.TestHarness.WPF.ViewModels
{
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
        private SecureString _securePassword;
        private bool _isPasswordLocked;

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

        public SecureString SecurePassword
        {
            get
            {
                return _securePassword;
            }
            set
            {
                SetProperty(ref _securePassword, value);
            } 
        }

        public List<ThemeMenuData> Accents { get; set; }

        public double SelectedFontSizeInPixels => _fontSizeInPoints*(DeviceIndependentPixelsPerInch/PointsPerInch);

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

        public RelayCommand OKCommand => new RelayCommand(nameof(OKCommand), param => Commit());

        public RelayCommand ClearFilesCommand
        {
            get
            {
                return new RelayCommand(nameof(ClearFilesCommand), param => Trace.WriteLine("Clear Files Clicked."));
            }
        }

        public RelayCommand CancelCommand
        {
            get { return new RelayCommand(nameof(CancelCommand), param => RefreshSettings()); }
        }

        public FontFamily SelectedFontFamily
        {
            get { return _selectedFontFamily; }
            set
            {
                SetProperty(ref _selectedFontFamily, value);
            }
        }

        public FontStyle SelectedFontStyle
        {
            get { return _selectedFontStyle; }
            set
            {
                SetProperty(ref _selectedFontStyle, value);
            }
        }

        public FontWeight SelectedFontWeight
        {
            get { return _selectedFontWeight; }
            set
            {
                SetProperty(ref _selectedFontWeight, value);
            }
        }

        public bool IsBoldChecked
        {
            get { return _boldChecked; }
            set
            {
                SetProperty(ref _boldChecked, value);
                SelectedFontWeight = _boldChecked ? FontWeights.Bold : FontWeights.Normal;
            }
        }

        public bool IsItalicChecked
        {
            get { return _italicChecked; }
            set
            {
                SetProperty(ref _italicChecked, value);
                //if (_italicChecked == value) return;
                //_italicChecked = value;

                SelectedFontStyle = _italicChecked ? FontStyles.Italic : FontStyles.Normal;
                //OnPropertyChanged();
            }
        }

        public bool IsKeepRecentFileListChecked
        {
            get { return _isKeepRecentFileListChecked; }
            set
            {
                SetProperty(ref _isKeepRecentFileListChecked, value);
            }
        }

        public bool IsPasswordLocked {
            get { return _isPasswordLocked; }
            set
            {
                SetProperty(ref _isPasswordLocked, value);
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
            SelectedTheme = Themes.FirstOrDefault(x => x.Name == Settings.Default.Theme);
            SelectedAccent = Accents.FirstOrDefault(x => x.Name == Settings.Default.Accent);

            SecurePassword = Settings.Default.Password;
            IsPasswordLocked = Settings.Default.IsPasswordLocked;

            SelectedFontFamily = Settings.Default.FontFamily ?? new FontFamily("Arial");
            SelectedFontSizeInPoints = Math.Abs(Settings.Default.FontSize) < double.Epsilon
                ? 28
                : Settings.Default.FontSize;
            IsBoldChecked = Settings.Default.FontBold;
            IsItalicChecked = Settings.Default.FontItalic;

            IsKeepRecentFileListChecked = Settings.Default.KeepRecentFilesList;
        }

        private void Commit()
        {
            if (_securePassword != null)
            {
                Settings.Default.Password = _securePassword;
            }
            Settings.Default.IsPasswordLocked = IsPasswordLocked;

            Settings.Default.ShowTooltips = ShowTooltips;
            Settings.Default.SplashOK = SplashOk;
            Settings.Default.Theme = SelectedTheme.Name;
            Settings.Default.Accent = SelectedAccent.Name;
            Settings.Default.FontFamily = SelectedFontFamily;
            Settings.Default.FontSize = SelectedFontSizeInPoints;
            Settings.Default.FontBold = IsBoldChecked;
            Settings.Default.FontItalic = IsItalicChecked;
            Settings.Default.KeepRecentFilesList = IsKeepRecentFileListChecked;
            
            // Save the settings
            Settings.Default.Save();
        }
    }
}