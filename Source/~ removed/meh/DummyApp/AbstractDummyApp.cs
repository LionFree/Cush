using System.Diagnostics;

namespace meh.DummyApp
{
    internal abstract class AbstractDummyApplication
    {
        public abstract void Run(params string[] args);

        public static AbstractDummyApplication ComposeObjectGraph()
        {
            return GetInstance(
                new DummyModel(),
                new DummyView(),
                new DummyViewModel());
        }

        internal static AbstractDummyApplication GetInstance(DummyModel model, DummyView view, DummyViewModel vm)
        {
            return new AbstractAppImplementation(model, view, vm);
        }

        private class AbstractAppImplementation : AbstractDummyApplication
        {
            private readonly DummyModel _model;
            private readonly DummyView _view;
            private readonly DummyViewModel _viewModel;

            internal AbstractAppImplementation(DummyModel model, DummyView view, DummyViewModel vm)
            {
                _model = model;
                _view = view;
                _viewModel = vm;
            }

            public override void Run(params string[] args)
            {
                Debug.WriteLine("AbstractDummyApplication Started!");
            }
        }
    }
}