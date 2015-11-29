using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Cush.Common.Logging;
using Cush.TestHarness.WPF.ViewModels;
using Cush.TestHarness.WPF.Views;
using Cush.Windows.SingleInstance;
using Cush.WPF;

namespace Cush.TestHarness.WPF.Infrastructure
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public abstract partial class Application : ISingleInstanceApplication
    {
        /// <summary>
        ///     Handle arguments passed from an attempt
        ///     to create a new instance of the app.
        /// </summary>
        /// <param name="args">
        ///     List of arguments to supply the first
        ///     instance of the application.
        /// </param>
        /// <returns>
        ///     <c>true</c> if successful, otherwise <c>false</c>.
        /// </returns>
        public abstract bool OnSecondInstanceCreated(string[] args);

        /// <summary>
        ///     The application entry point.
        ///     Initializes the application and calls the <see cref="M:Run()" /> method.
        /// </summary>
        public abstract void Start(params string[] args);

        /// <summary>
        ///     The method that will run when the entry point method
        ///     (<see cref="Start" />) tells the application to Run().
        /// </summary>
        protected abstract void App_OnStartup(object sender, StartupEventArgs e);

        public static Application ComposeObjectGraph()
        {
            return new ApplicationImplementation(
                new TestExceptionHandler(new Dialogs(), new NullLogger()),
                new MainView(new MainViewModel())
                );
        }

        private class ApplicationImplementation : Application
        {
            private readonly TestExceptionHandler _exceptionHandler;
            private readonly MainView _mainView;

            internal ApplicationImplementation(TestExceptionHandler handler, MainView view)
            {
                _exceptionHandler = handler;
                _mainView = view;
            }

            public override bool OnSecondInstanceCreated(string[] args)
            {
                var newArgs = args.ToList();

                // The first argument is always the executable path.
                newArgs.RemoveAt(0);
                //return _commandLine.Process(newArgs.ToArray());

                var argString = newArgs.Aggregate(args[0], (current, arg) => current + (", " + arg));

                _mainView.SetOnTop();

                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                {
                    MessageBox.Show(_mainView,
                        "Attempted to create second instance of application.  Command Line args received:\r\n" +
                        argString);
                }));

                //_mainView.Activate();
                return true;
            }

            public override void Start(params string[] args)
            {
                //_args = args;
                //if (!_commandLine.Process(_args)) return;

                try
                {
                    _exceptionHandler.AttachToApplication();
                    InitializeComponent();
                    Run();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Trace.WriteLine(ex.Message);
                    //_logger.ErrorException(ex);
                }
            }

            protected override void App_OnStartup(object sender, StartupEventArgs e)
            {
                Trace.WriteLine("App_OnStartup.");

                _mainView.Show();

                //Service_Start();
                //Service_Stop();
                //GenericRandom_Show();

                //OpenHost<PatientService>(8090, false, "rest");
                //Randoms_Show();

                //Test_ApplicationType();

                //var view = new MainView(new MainViewModel());
                //view.Show();

                //Shutdown();
            }
        }
    }
}