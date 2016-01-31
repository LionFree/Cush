using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;
using Cush.Common.Exceptions;
using Cush.WPF.Controls.Annotations;

namespace Cush.WPF.Controls
{
    /// <summary>
    ///     Interaction logic for ProgressDialog.xaml
    /// </summary>
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed partial class ProgressDialog : INotifyPropertyChanged
    {
        public static readonly DependencyProperty MessageVisibleProperty = DependencyProperty.Register(
            "MessageVisible", typeof (bool), typeof (ProgressDialog), new PropertyMetadata(false));

        public static readonly DependencyProperty CancelButtonTextProperty = DependencyProperty.Register(
            "CancelButtonText", typeof (string), typeof (ProgressDialog), new PropertyMetadata(Strings.BUTTON_Cancel));

        public static readonly DependencyProperty CancelActionTextProperty = DependencyProperty.Register(
            "CancelActionText", typeof (string), typeof (ProgressDialog),
            new PropertyMetadata(Strings.TEXT_CancelAction));

        public static readonly DependencyProperty ProgressProperty = DependencyProperty.Register(
            "Progress", typeof (double), typeof (ProgressDialog),
            new PropertyMetadata((double) 0, OnProgressPropertyChanged));

        public static readonly DependencyProperty CanCancelProperty = DependencyProperty.Register(
            "CanCancel", typeof(bool), typeof(ProgressDialog),
            new PropertyMetadata(true, OnCanCancelPropertyChanged));
        
        private volatile bool _isBusy;
        private BackgroundWorker _worker;

        public ProgressDialog(CushWindow owningWindow, ProgressDialogSettings settings) : base(owningWindow, settings)
        {
            InitializeComponent();

            if (settings == null)
                settings = ProgressDialogSettings.Cancellable;

            CancelButton.Visibility = settings.ShowCancelButton ? Visibility.Visible : Visibility.Collapsed;
        }

        public string Title
        {
            get { return TitleLabel.Text; }
            set { TitleLabel.Text = value; }
        }

        public string Message
        {
            get { return MessageLabel.Text; }
            set
            {
                MessageLabel.Text = value;
                MessageVisible = !string.IsNullOrEmpty(value);
            }
        }

        public string CancelButtonText
        {
            get { return (string) GetValue(CancelButtonTextProperty); }
            set { SetValue(CancelButtonTextProperty, value); }
        }

        public string CancelActionText
        {
            get { return (string) GetValue(CancelActionTextProperty); }
            set { SetValue(CancelActionTextProperty, value); }
        }

        public bool MessageVisible
        {
            get { return (bool) GetValue(MessageVisibleProperty); }
            set { SetValue(MessageVisibleProperty, value); }
        }

        public double Progress
        {
            get { return (double)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }

        public bool CanCancel
        {
            get { return (bool)GetValue(CanCancelProperty); }
            set { SetValue(CanCancelProperty, value); }
        }

        public void Reset()
        {
            Progress = 0;
            CanCancel = true;
            Message = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private static void OnProgressPropertyChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            var progressDialog = dependencyObject as ProgressDialog;
            progressDialog?.OnPropertyChanged(nameof(Progress));
            progressDialog?.OnProgressPropertyChanged(e);
        }

        private static void OnCanCancelPropertyChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            var progressDialog = dependencyObject as ProgressDialog;
            progressDialog?.OnPropertyChanged(nameof(Cancel));
            progressDialog?.OnCanCancelPropertyChanged(e);
        }

        private void OnCanCancelPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            CancelButton.IsEnabled = (bool)e.NewValue;
            var updateBarDelegate = new UpdateProgressDelegate(Determinate.SetValue);
            Dispatcher.Invoke(updateBarDelegate,
                DispatcherPriority.Background, IsEnabledProperty, (bool)e.NewValue);
        }

        private void OnProgressPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            Determinate.Value = (double)e.NewValue;
            var updateBarDelegate = new UpdateProgressDelegate(Determinate.SetValue);
            Dispatcher.Invoke(updateBarDelegate, DispatcherPriority.Background, RangeBase.ValueProperty, (double)e.NewValue);
        }

        private void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {
            Message = CancelActionText;
            MessageLabel.Visibility = Visibility.Visible;
            CanCancel = false;
            Cancel?.Invoke(this, new EventArgs());

            if (_worker == null) return;
            if (!_worker.WorkerSupportsCancellation) return;
            _worker.CancelAsync();
        }

        [SuppressMessage("ReSharper", "EventNeverSubscribedTo.Global")]
        public event EventHandler Cancel;

        internal ProgressDialogResult Execute(object operation)
        {
            ThrowHelper.IfNullThenThrow(() => operation);
            ProgressDialogResult result = null;

            _isBusy = true;

            _worker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

            _worker.DoWork +=
                (s, e) =>
                {
                    var action = operation as Action;
                    if (action != null)
                        action();
                    else if (operation is Action<BackgroundWorker>)
                        ((Action<BackgroundWorker>) operation)(s as BackgroundWorker);
                    else if (operation is Action<BackgroundWorker, DoWorkEventArgs>)
                        ((Action<BackgroundWorker, DoWorkEventArgs>) operation)(s as BackgroundWorker, e);
                    else if (operation is Func<object>)
                        e.Result = ((Func<object>) operation)();
                    else if (operation is Func<BackgroundWorker, object>)
                        e.Result = ((Func<BackgroundWorker, object>) operation)(s as BackgroundWorker);
                    else if (operation is Func<BackgroundWorker, DoWorkEventArgs, object>)
                        e.Result = ((Func<BackgroundWorker, DoWorkEventArgs, object>) operation)(s as BackgroundWorker,
                            e);
                    else
                        throw new InvalidOperationException();
                };

            _worker.RunWorkerCompleted +=
                (s, e) =>
                {
                    result = new ProgressDialogResult(e);
                    Dispatcher.BeginInvoke(DispatcherPriority.Send, (SendOrPostCallback) delegate
                    {
                        _isBusy = false;
                        OnClosing(this, new CancelEventArgs());
                        Close();
                    }, null);
                };

            _worker.ProgressChanged +=
                (s, e) =>
                {
                    if (_worker.CancellationPending) return;
                    Message = (e.UserState as string) ?? string.Empty;
                    Determinate.Value = e.ProgressPercentage;
                    Determinate.UpdateLayout();
                };

            _worker.RunWorkerAsync();

            OwningWindow.ShowDialogAsync(this);

            return result;
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private void OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = _isBusy;
        }

        internal static ProgressDialogResult Execute(CushWindow owner, string label,
            Action<BackgroundWorker, DoWorkEventArgs> operation, ProgressDialogSettings settings)
        {
            return ExecuteInternal(owner, label, operation, settings);
        }

        internal static ProgressDialogResult ExecuteInternal(CushWindow owner, string title, object operation,
            ProgressDialogSettings settings)
        {
            var dialog = new ProgressDialog(owner, settings);

            if (!string.IsNullOrEmpty(title))
                dialog.Title = title;

            return dialog.Execute(operation);
        }


        internal static bool CheckForPendingCancellation(BackgroundWorker worker, DoWorkEventArgs e)
        {
            if (worker.WorkerSupportsCancellation && worker.CancellationPending)
                e.Cancel = true;

            return e.Cancel;
        }


        internal static bool ReportWithCancellationCheck(BackgroundWorker worker, DoWorkEventArgs e, int percentProgress,
            string format, params object[] arg)
        {
            if (CheckForPendingCancellation(worker, e))
                return true;

            if (worker.WorkerReportsProgress)
                worker.ReportProgress(percentProgress, string.Format(format, arg));

            return false;
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private delegate void UpdateProgressDelegate(DependencyProperty dp, object value);
    }
}