using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Cush.Common.Async
{
    public abstract class AsyncWorker
    {
        /// <summary>
        ///     Creates a new <see cref="AsyncWorker" />,
        ///     using the default values.
        /// </summary>
        public static AsyncWorker Default
        {
            get { return new AsyncWorkerImplementation(); }
        }

        #region Abstract Members

        /// <summary>
        ///     Starts execution of a background operation.
        /// </summary>
        /// <param name="doWork">
        ///     Represents the method that will handle the
        ///     <see cref="E:System.ComponentModel.BackgroundWorker.DoWork" />
        ///     event.
        /// </param>
        /// <param name="onComplete">
        ///     Represents the method that will handle the
        ///     <see cref="E:System.ComponentModel.BackgroundWorker.RunWorkerCompleted" />
        ///     event.
        /// </param>
        /// <param name="progressChanged">
        ///     Represents the method that will handle the
        ///     <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" />
        ///     event.
        /// </param>
        /// <param name="arguments">
        ///     A <see cref="List&lt;Object&gt;" /> parameter for use by the background
        ///     operation to be executed in the <paramref name="doWork" /> method.
        /// </param>
        /// <exception cref="T:System.InvalidOperationException">
        ///     <see cref="P:System.ComponentModel.BackgroundWorker.IsBusy" /> is true.
        /// </exception>
        public abstract void Run(DoWorkEventHandler doWork, RunWorkerCompletedEventHandler onComplete,
            ProgressChangedEventHandler progressChanged, List<object> arguments = null);

        /// <summary>
        ///     Starts execution of the
        ///     <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" />
        ///     event handling method.
        /// </summary>
        /// <param name="percentProgress">
        ///     The percentage, from 0 to 100, of the background operation that is complete.
        /// </param>
        /// <param name="userState">
        ///     A state object for use in the
        ///     <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" />
        ///     event handling method.
        /// </param>
        public abstract void ReportProgress(int percentProgress, object userState);

        #endregion

        #region Overrides

        /// <summary>
        ///     Starts execution of a background operation.
        /// </summary>
        /// <param name="doWork">
        ///     Represents the method that will handle the
        ///     <see cref="E:System.ComponentModel.BackgroundWorker.DoWork" />
        ///     event.
        /// </param>
        /// <param name="arguments">
        ///     A <see cref="List&lt;Object&gt;" /> parameter for use by the background operation to
        ///     be executed in the <paramref name="doWork" /> method.
        /// </param>
        /// <exception cref="T:System.InvalidOperationException">
        ///     <see cref="P:System.ComponentModel.BackgroundWorker.IsBusy" /> is true.
        /// </exception>
        public void Run(DoWorkEventHandler doWork, List<object> arguments = null)
        {
            Run(doWork, null, null, arguments);
        }

        /// <summary>
        ///     Starts execution of a background operation.
        /// </summary>
        /// <param name="doWork">
        ///     Represents the method that will handle the
        ///     <see cref="E:System.ComponentModel.BackgroundWorker.DoWork" />
        ///     event.
        /// </param>
        /// <param name="onComplete">
        ///     Represents the mothod that will handle the
        ///     <see cref="E:System.ComponentModel.BackgroundWorker.RunWorkerCompleted" />
        ///     event.
        /// </param>
        /// <param name="arguments">
        ///     A <see cref="List&lt;Object&gt;" /> parameter for use by the background
        ///     operation to be executed in the <paramref name="doWork" /> method.
        /// </param>
        /// <exception cref="T:System.InvalidOperationException">
        ///     <see cref="P:System.ComponentModel.BackgroundWorker.IsBusy" /> is true.
        /// </exception>
        public void Run(DoWorkEventHandler doWork, RunWorkerCompletedEventHandler onComplete,
            List<object> arguments = null)
        {
            Run(doWork, onComplete, null, arguments);
        }

        /// <summary>
        ///     Starts execution of the
        ///     <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" />
        ///     event handling method.
        /// </summary>
        /// <param name="percentProgress">
        ///     The percentage, from 0 to 100, of the background operation that is complete.
        /// </param>
        public void ReportProgress(int percentProgress)
        {
            ReportProgress(percentProgress, null);
        }

        #endregion

        private sealed class AsyncWorkerImplementation : AsyncWorker
        {
            private bool _progressChanging;
            private BackgroundWorker _worker;

            public override void Run(DoWorkEventHandler doWork, RunWorkerCompletedEventHandler onComplete,
                ProgressChangedEventHandler progressChanged, List<object> arguments = null)
            {
                _worker = new BackgroundWorker
                {
                    WorkerReportsProgress = (progressChanged != null),
                    WorkerSupportsCancellation = false
                };

                _worker.DoWork += doWork;
                if (progressChanged != null)
                {
                    _worker.ProgressChanged += progressChanged;
                    _progressChanging = true;
                }
                if (onComplete != null) _worker.RunWorkerCompleted += onComplete;

                _worker.RunWorkerAsync(arguments);
            }

            public override void ReportProgress(int percentProgress, object userState)
            {
                if (!_progressChanging)
                    throw new InvalidOperationException("This worker does not support progress reporting.");

                _worker.ReportProgress(percentProgress, userState);
            }
        }
    }
}