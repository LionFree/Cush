using System.Diagnostics;
using meh.Infrastructure;

namespace meh.DummyApp
{
    internal class DummyApp1 : IApplication<DummyApp1>
    {
        private DummyModel _model;
        private DummyView _view;
        private DummyViewModel _viewModel;

        public DummyApp1()
        {
        }

        internal DummyApp1(DummyModel model, DummyView view, DummyViewModel vm)
        {
            _model = model;
            _view = view;
            _viewModel = vm;
        }

        public void Run(params string[] args)
        {
            if (null == args || args.Length == 0) args = new[] {"DummyWPFApplication"};

            Debug.WriteLine(args[0] + " Started!");
        }

        public DummyApp1 ComposeObjectGraph()
        {
            return new DummyApp1(
                new DummyModel(),
                new DummyView(),
                new DummyViewModel());
        }
    }
}