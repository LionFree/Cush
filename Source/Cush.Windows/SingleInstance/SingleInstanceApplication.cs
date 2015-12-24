using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using Cush.Common.Logging;

namespace Cush.Windows.SingleInstance
{
    public abstract class SingleInstanceApplication : Application, ISingleInstanceApplication
    {
        private readonly ILogger _logger;

        [SuppressMessage("ReSharper", "PublicConstructorInAbstractClass")]
        public SingleInstanceApplication() : this(Loggers.Trace)
        {
        }

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        protected SingleInstanceApplication(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        ///     Handle arguments passed from an attempt
        ///     to create a new instance of the application.
        /// </summary>
        /// <param name="args">
        ///     List of arguments to supply the first
        ///     instance of the application.
        /// </param>
        /// <returns>
        ///     <c>true</c> if successful, otherwise <c>false</c>.
        /// </returns>
        public virtual bool OnSecondInstanceCreated(string[] args)
        {
            return true;
        }

        /// <summary>
        ///     The application entry point.
        ///     Initializes the application and calls the <see cref="M:Run()" /> method.
        /// </summary>
        public void InitializeAndRun(params string[] args)
        {
            try
            {
                //InitializeComponent();
                //Run(null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        /// <summary>
        ///     The method that will run when the entry point method
        ///     (<see cref="M:InitializeAndRun" />) tells the application to Run().
        /// </summary>
        protected abstract void Start(object sender, StartupEventArgs e);
    }
}