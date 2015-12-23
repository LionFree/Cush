using System.Windows;
using Cush.Common.Interaction;

namespace Cush.TestHarness.WPF.Infrastructure
{
    internal class Dialogs : IDialogs<Window>
    {
        public void ShowError(string message)
        {
            ShowMessage(null, "Error", message, MessageBoxImage.Error);
        }

        public void ShowError(Window owner, string message)
        {
            ShowMessage(owner, "Error", message, MessageBoxImage.Information);
        }

        public void ShowMessage(Window owner, string title, string message, object icon)
        {
            var image = GetImageOrDefault(icon, MessageBoxImage.Information);
            
            if (owner == null)
            {
                MessageBox.Show(message, title, MessageBoxButton.OK, image, MessageBoxResult.None,
                    MessageBoxOptions.ServiceNotification);
                return;
            }

            MessageBox.Show(owner, message, title, MessageBoxButton.OK, image, MessageBoxResult.None);
        }



        private static MessageBoxImage GetImageOrDefault(object icon, MessageBoxImage defaultImage)
        {
            if (icon is MessageBoxImage)
                return (MessageBoxImage)icon;

            return defaultImage;
        }
    }
}