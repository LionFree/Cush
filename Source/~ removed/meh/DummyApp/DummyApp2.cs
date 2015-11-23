using System.Diagnostics;
using meh.Infrastructure;

namespace meh.DummyApp
{
    internal class DummyApp2 : IApplication
    {
        private readonly DummyModel _model;
        private readonly DummyView _view;
        private readonly DummyViewModel _viewModel;

        internal DummyApp2(DummyModel model, DummyView view, DummyViewModel vm)
        {
            _model = model;
            _view = view;
            _viewModel = vm;
        }

        public void Run(params string[] args)
        {
            Debug.WriteLine("DummyApp2 Started!");
        }

        public static DummyApp2 ComposeObjectGraph()
        {
            return new DummyApp2(
                new DummyModel(),
                new DummyView(),
                new DummyViewModel());
        }
    }
}