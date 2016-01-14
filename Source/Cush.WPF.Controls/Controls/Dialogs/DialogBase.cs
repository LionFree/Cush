using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Cush.WPF.ColorSchemes;

namespace Cush.WPF.Controls
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "VirtualMemberNeverOverriden.Global")]
    public abstract class DialogBase : ContentControl, ISchemedElement
    {
        public static readonly DependencyProperty DialogTopProperty = DependencyProperty.Register("DialogTop",
            typeof (object), typeof (DialogBase), new PropertyMetadata(null));

        public static readonly DependencyProperty DialogBottomProperty = DependencyProperty.Register("DialogBottom",
            typeof (object), typeof (DialogBase), new PropertyMetadata(null));

        static DialogBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (DialogBase),
                new FrameworkPropertyMetadata(typeof (DialogBase)));
        }

        /// <summary>
        ///     Initializes a new DialogBase.
        /// </summary>
        /// <param name="owningWindow">The window that is the parent of the dialog.</param>
        /// <param name="settings"></param>
        protected DialogBase(CushWindow owningWindow, DialogSettings settings)
        {
            DialogSettings = settings ?? owningWindow?.DialogOptions ?? new DialogSettings();
            OwningWindow = owningWindow;
            Initialize();
        }

        /// <summary>
        ///     Initializes a new DialogBase with the given settings and no owning window.
        /// </summary>
        protected DialogBase(DialogSettings settings) : this(null, settings)
        {
        }

        /// <summary>
        ///     Initializes a new DialogBase with default settings and no owning window.
        /// </summary>
        protected DialogBase() : this(null, new DialogSettings())
        {
        }

        /// <summary>
        ///     Gets/sets the dialog's Guid (it's 'handle').
        /// </summary>
        internal Guid Guid { get; set; }

        /// <summary>
        ///     Gets/sets arbitrary content on top of the dialog.
        /// </summary>
        public object DialogTop
        {
            get { return GetValue(DialogTopProperty); }
            set { SetValue(DialogTopProperty, value); }
        }

        /// <summary>
        ///     Gets/sets arbitrary content below the dialog.
        /// </summary>
        public object DialogBottom
        {
            get { return GetValue(DialogBottomProperty); }
            set { SetValue(DialogBottomProperty, value); }
        }

        internal SizeChangedEventHandler SizeChangedHandler { get; set; }

        public DialogSettings DialogSettings { get; private set; }

        /// <summary>
        ///     Gets or sets the window that owns the current Dialog.
        /// </summary>
        [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
        protected CushWindow OwningWindow { get; set; }

        /// <summary>
        ///     Gets or sets the current <see cref="T:IColorScheme" />.
        /// </summary>
        public IColorScheme ColorScheme { get; set; }


        private void Initialize()
        {
            Unloaded += Dialog_Unloaded;
            ColorSchemeManager.Register(this);
        }

        public virtual void OnClose()
        {
        }

        [SuppressMessage("ReSharper", "VirtualMemberNeverOverriden.Global")]
        public virtual void OnShown()
        {
        }

        /// <summary>
        ///     A last chance virtual method for stopping an external dialog from closing.
        /// </summary>
        /// <returns></returns>
        [SuppressMessage("ReSharper", "VirtualMemberNeverOverriden.Global")]
        protected virtual bool OnRequestClose()
        {
            return true; //allow the dialog to close.
        }

        private void Dialog_Unloaded(object sender, RoutedEventArgs e)
        {
            //ThemeManager.IsThemeChanged -= ThemeManager_IsThemeChanged;
            Unloaded -= Dialog_Unloaded;
        }

        public Task WaitForCloseAsync()
        {
            var tcs = new TaskCompletionSource<object>();

            if (DialogSettings.AnimateHide)
            {
                var closingStoryboard = Resources["DialogCloseStoryboard"] as Storyboard;

                if (closingStoryboard == null)
                    throw new InvalidOperationException(
                        "Unable to find the dialog closing storyboard. Did you forget to add ContentDialog.xaml to your merged dictionaries?");

                EventHandler handler = null;
                var storyboard = closingStoryboard;
                handler = (sender, args) =>
                {
                    // ReSharper disable once AccessToModifiedClosure
                    storyboard.Completed -= handler;
                    tcs.TrySetResult(null);
                };

                closingStoryboard = closingStoryboard.Clone();

                closingStoryboard.Completed += handler;

                closingStoryboard.Begin(this);
            }
            else
            {
                Opacity = 0.0;
                tcs.TrySetResult(null); //skip the animation
            }

            return tcs.Task;
        }

        /// <summary>
        ///     Closes the dialog.
        /// </summary>
        [SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
        public Task Close()
        {
            return RequestCloseAsync();
        }

        /// <summary>
        ///     Waits for the dialog to become ready for interaction.
        /// </summary>
        /// <returns>A task that represents the operation and it's status.</returns>
        public Task WaitForLoadAsync()
        {
            Dispatcher.VerifyAccess();

            if (IsLoaded) return new Task(() => { });

            if (!DialogSettings.AnimateShow)
            {
                Opacity = 1.0; //skip the animation
            }

            var tcs = new TaskCompletionSource<object>();

            RoutedEventHandler handler = null;
            handler = (sender, args) =>
            {
                Loaded -= handler;
                tcs.TrySetResult(null);
            };

            Loaded += handler;

            return tcs.Task;
        }

        /// <summary>
        ///     Requests an externally shown Dialog to close. Will throw an exception if the Dialog is inside of a MetroWindow.
        /// </summary>
        private Task RequestCloseAsync()
        {
            return OnRequestClose() ? OwningWindow.HideDialogAsync(this) : Task.Factory.StartNew(() => { });
        }

        /// <summary>
        ///     Waits until this dialog gets unloaded.
        /// </summary>
        /// <returns></returns>
        public Task WaitUntilUnloadedAsync()
        {
            var tcs = new TaskCompletionSource<object>();

            Unloaded += (s, e) => { tcs.TrySetResult(null); };

            return tcs.Task;
        }
    }
}