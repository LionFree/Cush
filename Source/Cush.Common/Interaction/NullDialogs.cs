namespace Cush.Common.Interaction
{
    public abstract class NullDialogs : IDialogs
    {
        private static readonly IDialogs PrivateDefault;
        public abstract void ShowError(string message);
        public abstract void ShowError<T>(T owner, string message);
        public abstract void ShowMessage<T>(T owner, string title, string message, object icon);

        static NullDialogs()
        {
            PrivateDefault = new NullDialogsImpl();
        }

        public static IDialogs Default
        {
            get { return PrivateDefault; }
        }
        
        private class NullDialogsImpl : NullDialogs
        {
            public override void ShowError(string message)
            {
                // Do nothing.
            }

            public override void ShowError<T>(T owner, string message)
            {
                // Do nothing.
            }

            public override void ShowMessage<T>(T owner, string title, string message, object icon)
            {
                // Do nothing.
            }
        }
    }
}