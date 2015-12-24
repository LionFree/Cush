using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Cush.Common.Exceptions;
using Cush.WPF.ColorSchemes;

namespace Cush.WPF.Controls
{
    public static class DialogManager
    {
        public delegate void DialogStateChangedHandler(object sender, DialogStateChangedEventArgs args);

        [SuppressMessage("ReSharper", "EventNeverSubscribedTo.Global")]
        public static event DialogStateChangedHandler DialogOpened;

        [SuppressMessage("ReSharper", "EventNeverSubscribedTo.Global")]
        public static event DialogStateChangedHandler DialogClosed;

        private static ContentDialog GetDialog(CushWindow window, ContentDialog content, DialogSettings settings)
        {
            // Return the existing dialog, if it exists. 
            // Otherwise, create a new one.
            return window.DialogContainer.Children.Cast<ContentDialog>()
                .FirstOrDefault(item => item.Guid == content.Guid)
                         ?? CreateDialog(window, content, settings);
        }

        private static ContentDialog CreateDialog(CushWindow window, ContentDialog content, DialogSettings settings)
        {
            if (settings == null)
                settings = window.DialogOptions;
            
            // Pull the content out of the dialog and put it into a new dialog.  
            // (For some reason, using the dialog directly doesn't work.)
            var body = content;
            var bodyContext = (content).DataContext;

            //create the dialog control
            var dialog = new ContentDialog(window, settings)
            {
                Content = body,
                Guid = content.Guid,
                DataContext = bodyContext,
            };

            return dialog;
        }

        /// <summary>
        ///     Creates a ContentDialog inside of the current window.
        /// </summary>
        /// <param name="window"></param>
        /// <param name="content">The ContentDialog to display.</param>
        /// <param name="settings">Optional settings that override the global metro dialog settings.</param>
        /// <returns>A task promising the result of which button was pressed.</returns>
        public static Task<MessageDialogResult> ShowDialogAsync(this CushWindow window, ContentDialog content,
            DialogSettings settings = null)
        {
            ThrowHelper.IfNullThenThrow(() => content);

            window.Dispatcher.VerifyAccess();
            return HandleOverlayOnShow(settings, window).ContinueWith(z => window.Dispatcher.Invoke(() =>
            {
                var dialog = GetDialog(window, content, settings);
                var sizeHandler = SetupAndOpenDialog(window, dialog);
                dialog.SizeChangedHandler = sizeHandler;

                return dialog.WaitForLoadAsync().ContinueWith(x =>
                {
                    window.OnDialogOpened();
                    return dialog.WaitForButtonPressAsync().ContinueWith(y =>
                    {
                        //once a button as been clicked, begin removing the dialog.
                        dialog.OnClose();
                        window.OnDialogClosed();

                        var closingTask = window.Dispatcher.Invoke(dialog._WaitForCloseAsync);

                        return closingTask.ContinueWith(a => (window.Dispatcher.Invoke(() =>
                        {
                            window.SizeChanged -= sizeHandler;

                            window.DialogContainer.Children.Remove(dialog);
                            //remove the dialog from the container

                            return HandleOverlayOnHide(settings, window);
                            //window.overlayBox.Visibility = System.Windows.Visibility.Hidden; //deactive the overlay effect
                        })).ContinueWith(y3 => y).Unwrap());
                    }).Unwrap();
                }).Unwrap().Unwrap();
            })).Unwrap();
        }

        

        private static void OnDialogOpened(this DispatcherObject window)
        {
            if (DialogOpened != null)
            {
                window.Dispatcher.BeginInvoke(
                    new Action(() => DialogOpened(window, new DialogStateChangedEventArgs())));
            }
        }

        private static void OnDialogClosed(this DispatcherObject window)
        {
            if (DialogClosed != null)
            {
                window.Dispatcher.BeginInvoke(
                    new Action(() => DialogClosed(window, new DialogStateChangedEventArgs())));
            }
        }

        /// <summary>
        ///     Hides a visible dialog.
        /// </summary>
        /// <param name="window">The window with the dialog that is visible.</param>
        /// <param name="dialog">The dialog instance to hide.</param>
        /// <returns>A task representing the operation.</returns>
        /// <exception cref="InvalidOperationException">
        ///     The <paramref name="dialog" /> is not visible in the window.
        ///     This happens if <see cref="ShowDialogAsync" /> hasn't been called before.
        /// </exception>
        public static Task HideDialogAsync(this CushWindow window, DialogBase dialog)
        {
            window.Dispatcher.VerifyAccess();

            var foundDialog = false;

            var contentDialog = dialog as ContentDialog;
            if (contentDialog != null)
            {
                var dialog2 = contentDialog;

                foreach (ContentDialog item in window.DialogContainer.Children)
                {
                    if (item.Guid != dialog2.Guid) continue;
                    foundDialog = true;
                    dialog = item;
                    break;
                }
            }
            else
            {
                foundDialog = window.DialogContainer.Children.Contains(dialog);
            }

            if (!foundDialog)
                throw new InvalidOperationException("The provided dialog is not visible in the specified window.");

            window.SizeChanged -= dialog.SizeChangedHandler;

            dialog.OnClose();

            var closingTask = window.Dispatcher.Invoke(() => dialog._WaitForCloseAsync());
            return closingTask.ContinueWith(a =>
            {
                if (DialogClosed != null)
                {
                    window.Dispatcher.BeginInvoke(
                        new Action(() => DialogClosed(window, new DialogStateChangedEventArgs())));
                }

                return window.Dispatcher.Invoke(() =>
                {
                    window.DialogContainer.Children.Remove(dialog); //remove the dialog from the container
                    ColorSchemeManager.Release(dialog);
                    return window.HideOverlayAsync();
                });
            }).Unwrap();
        }

        private static Task HandleOverlayOnHide(DialogSettings settings, CushWindow window)
        {
            return (settings == null || settings.AnimateHide
                ? window.HideOverlayAsync()
                : Task.Factory.StartNew(() => window.Dispatcher.Invoke(window.HideOverlay)));
        }

        private static Task HandleOverlayOnShow(DialogSettings settings, CushWindow window)
        {
            return (settings == null || settings.AnimateShow
                ? window.ShowOverlayAsync()
                : Task.Factory.StartNew(() => window.Dispatcher.Invoke(window.ShowOverlay)));
        }

        private static SizeChangedEventHandler SetupAndOpenDialog(CushWindow window, DialogBase dialog)
        {
            dialog.SetValue(Panel.ZIndexProperty, (int) window.OverlayBox.GetValue(Panel.ZIndexProperty) + 1);
            dialog.MinHeight = window.ActualHeight/4.0;
            dialog.MaxHeight = window.ActualHeight;

            //an event handler for auto resizing an open dialog.
            SizeChangedEventHandler sizeHandler = (sender, args) =>
            {
                dialog.MinHeight = window.ActualHeight/4.0;
                dialog.MaxHeight = window.ActualHeight;
            };

            window.SizeChanged += sizeHandler;

            window.DialogContainer.Children.Add(dialog); //add the dialog to the container

            dialog.OnShown();

            return sizeHandler;
        }
    }
}