namespace Cush.Windows.SingleInstance
{
    public static class SingleInstanceExtensions
    {
        private static readonly SingleInstanceService SingleInstanceTracker;

        static SingleInstanceExtensions()
        {
            SingleInstanceTracker = SingleInstanceService.GetInstance();
        }

        public static void StartSingleInstance(this ISingleInstanceApplication app, string[] args)
        {
            if (!SingleInstanceTracker.InstantiateSingleInstance(() => app)) return;

            app.Start(args);

            SingleInstanceTracker.Cleanup();
        }
    }
}