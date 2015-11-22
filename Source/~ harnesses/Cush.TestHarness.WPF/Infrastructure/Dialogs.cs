using System.Windows;
using Cush.Common;
using Cush.Common.Interaction;

namespace Cush.TestHarness.WPF.Infrastructure
{
    internal class Dialogs : IDialogs
    {
        public void ShowError(string message)
        {
            ShowMessage<Window>(null, "Error", message, MessageBoxImage.Error);
        }

        public void ShowError<T>(T owner, string message)
        {
            ShowMessage(owner, "Error", message, MessageBoxImage.Information);
        }

        public void ShowMessage<T>(T owner, string title, string message, object icon)
        {
            var window = owner as Window;
            var image = GetImageOrDefault(icon, MessageBoxImage.Information);
            
            if (window == null)
            {
                MessageBox.Show(message, title, MessageBoxButton.OK, image, MessageBoxResult.None,
                    MessageBoxOptions.ServiceNotification);
                return;
            }

            MessageBox.Show(window, message, title, MessageBoxButton.OK, image, MessageBoxResult.None);
        }



        private static MessageBoxImage GetImageOrDefault(object icon, MessageBoxImage defaultImage)
        {
            if (icon is MessageBoxImage)
                return (MessageBoxImage)icon;

            return defaultImage;
        }
    }
}