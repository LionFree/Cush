using meh.Infrastructure;

namespace meh.DummyApp
{
    //internal abstract class CompositionRoot<T>
    //{
    //    protected static T App;

    //    public static T ObjectGraph
    //    {
    //        get { return App; }
    //        protected set { App = value; }
    //    }
    //}

    internal class CompRoot : AbstractCompositionRoot<DummyApp1>
    {
        public override DummyApp1 ComposeObjectGraph()
        {
            return new DummyApp1(
                new DummyModel(),
                new DummyView(),
                new DummyViewModel());
        }
    }
}