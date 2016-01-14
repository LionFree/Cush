namespace Cush.Common.Interaction
{
    public abstract class NullDialogs<TWindow> : IDialogs<TWindow>
    {
        private static readonly IDialogs<TWindow> PrivateDefault;

        static NullDialogs()
        {
            PrivateDefault = new NullDialogsImpl();
        }

        public static IDialogs<TWindow> Default
        {
            get { return PrivateDefault; }
        }

        public abstract void ShowError(string message);
        public abstract void ShowError(TWindow owner, string message);
        public abstract void ShowMessage(TWindow owner, string title, string message, object icon);

        private class NullDialogsImpl : NullDialogs<TWindow>
        {
            public override void ShowError(string message)
            {
                // Do nothing.
            }

            public override void ShowError(TWindow owner, string message)
            {
                // Do nothing.
            }

            public override void ShowMessage(TWindow owner, string title, string message, object icon)
            {
                // Do nothing.
            }
        }
    }
}