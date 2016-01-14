namespace Cush.Windows.SingleInstance
{
    public static class SingleInstanceExtensions
    {
        private static readonly SingleInstanceService SingleInstanceTracker;

        static SingleInstanceExtensions()
        {
            SingleInstanceTracker = SingleInstanceService.GetInstance();
        }

        /// <summary>
        ///     Starts an <see cref="ISingleInstanceApplication" />, and runs the
        ///     <see cref="M:Start" /> method, or if the application is already running,
        ///     passes the command-line arguments to the existing instance.
        /// </summary>
        public static void StartSingleInstance(this ISingleInstanceApplication app, string[] args)
        {
            if (!SingleInstanceTracker.InstantiateSingleInstance(() => app)) return;

            app.InitializeAndRun(args);

            SingleInstanceTracker.Cleanup();
        }
    }
}