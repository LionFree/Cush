using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;

namespace Cush.WPF.Controls
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class ContentDialog : DialogBase
    {
        
        public static readonly DependencyProperty DialogResultProperty = DependencyProperty.RegisterAttached(
            "DialogResult", typeof(bool?), typeof(ContentDialog), new PropertyMetadata(DialogResultChanged));
        
        static ContentDialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ContentDialog),
                                                     new FrameworkPropertyMetadata(typeof(ContentDialog)));
        }

        /// <summary>
        ///     Initializes a new ContentDialog.
        /// </summary>
        /// <param name="owningWindow">The window that is the parent of the dialog.</param>
        /// <param name="settings"></param>
        public ContentDialog(CushWindow owningWindow, DialogSettings settings) : base(owningWindow, settings)
        {
            Initialize();
        }

        /// <summary>
        ///     Initializes a new ContentDialog.
        /// </summary>
        public ContentDialog():this(null, null)
        {
        }

        private void Initialize()
        {
            Guid = Guid.NewGuid();
            Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/Cush.WPF.Controls;component/ControlTemplates/ContentDialog.xaml")
            });
        }

        internal Task<MessageDialogResult> WaitForButtonPressAsync()
        {
            Dispatcher.BeginInvoke(new Action(() => Focus()));

            var tcs = new TaskCompletionSource<MessageDialogResult>();

            return tcs.Task;
        }

        private static async void DialogResultChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dialog = d as ContentDialog;
            if (dialog != null) await dialog.Close();
        }

        public static void SetDialogResult(ContentDialog target, bool? value)
        {
            target.SetValue(DialogResultProperty, value);
        }

    }
}