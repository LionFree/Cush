namespace Cush.Windows.SingleInstance
{
    /// <summary>
    ///     Provides interface methods for wrapping a single-instance application.
    ///     NOTE: For WPF apps, when creating a new <see cref="M:Main()" /> method,
    ///     be sure to set the Build Action of the <see cref="F:App.xaml" /> to <see cref="T:Page" />;
    ///     this will prevent an error about multiple Main() methods.
    /// </summary>
    public interface ISingleInstanceApplication
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
        ///     <see langword="true" /> if successful,
        ///     otherwise <see langword="false" />.
        /// </returns>
        bool OnSecondInstanceCreated(string[] args);

        /// <summary>
        ///     The wrapped application's entry point.
        ///     Initializes the application and calls the <see cref="M:Run()" /> method.
        /// </summary>
        void InitializeAndRun(params string[] args);
    }
}