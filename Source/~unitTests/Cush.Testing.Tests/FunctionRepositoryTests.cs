using System;
using NUnit.Framework;

namespace Cush.Testing.Tests
{
    [TestFixture]
    public class FunctionRepositoryTests
    {
        [Test]
        public void Test_FR_GenericType_NoMethod()
        {
            var sut = FunctionRepository.GetInstance();

            Assert.Throws<ArgumentException>(() => sut.GetValue<double>("scooby"));
        }

        [Test]
        public void Test_FR_GenericType_WrongType()
        {
            var sut = FunctionRepository.GetInstance();
            sut.AddMethod(() => new float());

            Assert.Throws<ArgumentException>(() => sut.GetValue<double>());
        }

        [Test]
        public void Test_FR_GenericType_WrongType_MultipleArguments()
        {
            var sut = FunctionRepository.GetInstance();
            sut.AddMethod(() => new float());

            Assert.Throws<ArgumentException>(() => sut.GetValue<double>("scooby","doo"));
        }

        [Test]
        public void Test_FR_GenericType_WrongArguments()
        {
            var sut = FunctionRepository.GetInstance();
            sut.AddMethod(() => new float());

            Assert.Throws<ArgumentException>(() => sut.GetValue<float>("scooby", "doo"));
        }
    }
}