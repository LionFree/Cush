using System;
using System.Diagnostics;
using System.Windows;

namespace meh.meh
{
    public abstract partial class WPFApplication: Application
    {
        private string[] _args;

        protected WPFApplication()
        {

        }

        /// <summary>
        ///     The method that will run when the entry point method
        ///     (<see cref="Start" />) tells the application to Run().
        /// </summary>
        [STAThread]
        protected abstract void OnStartup(object sender, StartupEventArgs e);

        /// <summary>
        ///     The application entry point.
        /// </summary>
        public void Start(params string[] args)
        {
            _args = args;
            //if (!_commandLine.Process(_args)) return;

            try
            {
                //InitializeComponent();
                //Run();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                //_logger.ErrorException(ex);
            }
        }
    }
}