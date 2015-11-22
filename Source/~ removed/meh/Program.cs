using meh.DummyApp;
using meh.Infrastructure;

namespace meh
{
    class Program
    {
        public static void Main()
        {
            
        }
        internal static void Test_CompositionRoot(string[] args)
        {
            // Static CompositionRoot, typing handled by method.
            // requires app to have public parameterless constructor.
            // Introduces additional complexity
            var da3A = CompositionRoot.ComposeObjectGraph<DummyApp1>();
            da3A.Run("da3A");

            // Static CompositionRoot, typing handled by class.
            // requires app to have public parameterless constructor.
            // Introduces additional complexity
            var da3B = CompositionRoot<DummyApp1>.ComposeObjectGraph();
            da3B.Run("da3B");

            // Overridde method (on concrete implementation) of AbstractCompositionRoot class.
            // Introduces additional complexity
            var da4 = new CompRoot().ComposeObjectGraph();
            da4.Run("da4");

            // Added complexity
            Bootstrapper.ComposeObjectGraph<DummyApp1>().Run("Bootstrapper.Resolve");
            Bootstrapper<DummyApp1>.Run("Bootstrapper<T>.Run");

            // Static method on normal class.
            var da1 = DummyApp2.ComposeObjectGraph();
            da1.Run("DummyApp2");
            // Alternatively, do it all in one go:
            DummyApp2.ComposeObjectGraph().Run("DummyApp2 (chained)");

            // Static "GetInstance" method on an abstract class.
            var da2 = AbstractDummyApplication.ComposeObjectGraph();
            da2.Run(args);

            // Alternatively, do it all in one go:
            AbstractDummyApplication.ComposeObjectGraph().Run(args);
        }

        private static DummyApp2 ComposeObjectGraph()
        {
            return new DummyApp2(
                new DummyModel(),
                new DummyView(),
                new DummyViewModel());
        }

    }
}
