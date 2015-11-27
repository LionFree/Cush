using System;
using System.Windows;
using Moq;
using NUnit.Framework;

namespace Cush.MVVM.Tests
{
    [TestFixture]
    public class FactoryTests
    {
        internal class MockViewWithParameter : Window, IView
        {
            internal MockViewWithParameter(MockViewModel vm)
            {
                DataContext = vm;
            }
        }

        internal class MockViewWithoutParameter : Window, IView
        {
            public string Property { get; set; }
        }

        internal class MockViewModel : IViewModel
        {
            public string Property { get; set; }
        }

        internal class MockFactoryHarness
        {
            private readonly Factory<ViewModel> _factory;

            public MockFactoryHarness(Factory<ViewModel> factory)
            {
                _factory = factory;
            }

            public IViewModel TestObject { get; set; }

            internal void ActivateFactory()
            {
                TestObject = _factory.Create<IViewModel>();
            }
        }
        
        [Test, STAThread]
        public void Given_ConstructorWithoutParameter_When_NoParameterIsPassed_Then_Golden()
        {
            var viewFactory = Factory<View>.GetInstance();
            var vmFactory = Factory<ViewModel>.GetInstance();
            MockViewWithoutParameter view = null;

            Assert.IsNotNull(viewFactory);
            Assert.IsNotNull(vmFactory);
            Assert.DoesNotThrow(() => { view = viewFactory.Create<MockViewWithoutParameter>(); });
            Assert.IsNotNull(view);
            Assert.IsAssignableFrom<MockViewWithoutParameter>(view);
        }

        [Test, STAThread]
        public void Given_ConstructorWithoutParameter_When_ParameterIsPassed_Then_ExceptionIsThrown()
        {
            var viewFactory = Factory<View>.GetInstance();
            var vmFactory = Factory<ViewModel>.GetInstance();
            var vm = vmFactory.Create<MockViewModel>();

            Assert.IsNotNull(viewFactory);
            Assert.IsNotNull(vmFactory);
            Assert.Throws<MissingMethodException>(() => { viewFactory.Create<MockViewWithoutParameter>(vm); });
        }

        [Test, STAThread]
        public void Given_ConstructorWithParameter_When_NoParameterIsPassed_Then_ExceptionIsThrown()
        {
            var viewFactory = Factory<View>.GetInstance();
            var vmFactory = Factory<ViewModel>.GetInstance();

            Assert.IsNotNull(viewFactory);
            Assert.IsNotNull(vmFactory);
            Assert.Throws<MissingMethodException>(() => { viewFactory.Create<MockViewWithParameter>(); });
        }

        [Test, STAThread]
        public void Given_ConstructorWithParameter_When_ParameterIsPassed_Then_Golden()
        {
            var viewFactory = Factory<View>.GetInstance();
            var vmFactory = Factory<ViewModel>.GetInstance();

            var vm = vmFactory.Create<MockViewModel>();
            var view = viewFactory.Create<MockViewWithParameter>(vm);

            Assert.IsNotNull(viewFactory);
            Assert.IsNotNull(vmFactory);
            Assert.IsNotNull(view);
            Assert.IsNotNull(vm);
            Assert.AreEqual(vm, view.DataContext);
        }

        [Test, STAThread]
        public void Given_TryingToMockAFactory_When_UnitTesting_Then_Golden()
        {
            var expected = new MockViewModel {Property = "Shazam"};
            var mock = new Mock<Factory<ViewModel>>();
            mock.Setup(m => m.Create<IViewModel>()).Returns(expected);


            var sut = new MockFactoryHarness(mock.Object);
            sut.ActivateFactory();


            var actual = sut.TestObject;
            Assert.AreEqual(expected, actual);
        }
    }
}