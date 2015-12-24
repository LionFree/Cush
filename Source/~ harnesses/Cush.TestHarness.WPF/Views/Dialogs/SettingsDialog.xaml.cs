using System.ComponentModel;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Cush.TestHarness.WPF.Properties;
using Cush.TestHarness.WPF.ViewModels;
using Cush.WPF.ColorSchemes;
using Cush.WPF.Controls;

namespace Cush.TestHarness.WPF.Views.Dialogs
{
    /// <summary>
    ///     Interaction logic for SettingsDialog.xaml
    /// </summary>
    internal partial class SettingsDialog
    {
        //private readonly ConfigFile _configFile;
        private bool _ignorePasswordChanges;
        private bool _passwordChanged;
        private bool _schemeChanged;
        private IColorScheme _originalScheme;

        public SettingsDialog(ISettingsViewModel vm, CushWindow owningWindow, DialogSettings settings)
            : base(owningWindow, settings)
        {
            DataContext = vm;
            InitializeComponent();
            _originalScheme = new ColorScheme(CurrentScheme);

            if (vm.ConfigFileHasPassword)
                InitializePassword();

            Apply.IsEnabled = false;

            var cvs = CollectionViewSource.GetDefaultView(FontCombo.ItemsSource);
            cvs.SortDescriptions.Clear();
            cvs.SortDescriptions.Add(new SortDescription("Source", ListSortDirection.Ascending));
            cvs.Refresh();
        }

        private void InitializePassword()
        {
            _ignorePasswordChanges = true;
            PasswordBox.Password = "********";
            ConfirmBox.Password = "********";
            PasswordLocked.IsChecked = true;
            _ignorePasswordChanges = false;
        }

        private void SettingChanged(object sender, RoutedEventArgs e)
        {
            if (!IsInitialized) return;
            var samePassword = PasswordValidator.AreEqual(PasswordBox.SecurePassword, ConfirmBox.SecurePassword);
            Apply.IsEnabled = samePassword;
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!(IsInitialized) || _ignorePasswordChanges) return;

            ConfirmBox.Password = string.Empty;
            _passwordChanged = true;
            Apply.IsEnabled = false;
        }

        private void PasswordLocked_OnClick(object sender, RoutedEventArgs e)
        {
            if (!IsInitialized) return;
            if (PasswordLocked.IsChecked == false)
            {
                ClearPasswordCommand_OnExecuted(sender, null);
            }
            SettingChanged(null, null);
        }

        private void AccentChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;

            var data = e.AddedItems[0] as ThemeMenuData;
            if (data == null) return;

            ColorSchemeManager.SetManagedAccent(data.Name);
            _schemeChanged = true;

            if (IsInitialized) Apply.IsEnabled = true;
        }

        [SecurityCritical]
        private void ThemeChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;

            var data = e.AddedItems[0] as ThemeMenuData;
            if (data == null) return;

            ColorSchemeManager.SetManagedTheme(data.Name);
            _schemeChanged = true;

            UpdateDefaultStyle();
            UpdateLayout();

            if (IsInitialized) Apply.IsEnabled = true;
        }


        private void ClearPasswordCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _ignorePasswordChanges = true;
            PasswordBox.Password = "";
            ConfirmBox.Password = "";
            _passwordChanged = true;
            _ignorePasswordChanges = false;
        }

        [SecurityCritical]
        private void OnOkClick(object sender, RoutedEventArgs e)
        {
            OnApplyClick(sender, e);
            _originalScheme = new ColorScheme(CurrentScheme);
            Close();
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            if (_schemeChanged)
                ColorSchemeManager.SetManagedColorScheme(_originalScheme);

            Settings.Default.Reload();
            Close();
        }

        private void OnApplyClick(object sender, RoutedEventArgs e)
        {
            if (_passwordChanged)
            {
                //_configFile.SetPassword(PasswordBox.SecurePassword, PasswordValidator.CreateNewSalt());
                //_configFile.Save();
                _passwordChanged = false;
            }

            Apply.IsEnabled = false;
        }
    }
}