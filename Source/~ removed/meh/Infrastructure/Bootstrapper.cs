namespace meh.Infrastructure
{
    public abstract class Bootstrapper
    {
        public static T Resolve<T>() where T : ICompositionRoot<T>, new()
        {
            return ComposeObjectGraph<T>();
        }

        public static T ComposeObjectGraph<T>() where T : ICompositionRoot<T>, new()
        {
            return (new T()).ComposeObjectGraph();
        }
    }

    public static class Bootstrapper<T> where T : IApplication<T>, new()
    {
        private static readonly T App;

        static Bootstrapper()
        {
            App = Bootstrapper.Resolve<T>();
        }

        public static void Run(params string[] args)
        {
            App.Run(args);
        }
    }
}