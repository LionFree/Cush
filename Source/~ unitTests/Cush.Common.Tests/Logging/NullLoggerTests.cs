using System;
using Cush.Common.Logging;
using Cush.Testing;
using NUnit.Framework;

namespace Cush.Common.Tests.Logging
{
    [TestFixture]
    internal class NullLoggerTests
    {
        [SetUp]
        public void SetUp()
        {
            _sut = new NullLogger();
        }

        private ILogger _sut;

        private void TestMessage(Action<string> action)
        {
            var message = GetRandom.String(5, 20);
            Assert.DoesNotThrow(() => action(message));
        }

        private void TestFormattedException(Action<bool, Exception, string, object[]> action)
        {
            var logEvent = GetRandom.Bool();
            var exception = GetRandom.Exception();
            var messageFormat = "Stupid Exception: {0}";
            var args = new[]
            {
                GetRandom.String(2, 5),
                GetRandom.String(2, 5),
                GetRandom.String(2, 5)
            };

            Assert.DoesNotThrow(() => action(logEvent, exception, messageFormat, args));
        }

        private void TestException(Action<Exception, string> action)
        {
            var exception = GetRandom.Exception();
            var message = GetRandom.String(5, 20);
            
            Assert.DoesNotThrow(() => action(exception, message));
        }

        private void TestFormatted(Action<string, object[]> action)
        {
            var messageFormat = "Stupid Exception: {0}";
            var args = new[]
            {
                GetRandom.String(2, 5),
                GetRandom.String(2, 5),
                GetRandom.String(2, 5)
            };

            Assert.DoesNotThrow(() => action(messageFormat, args));
        }
        
        [Test]
        public void Constructor()
        {
            object temp = null;
            Assert.DoesNotThrow(() => { temp = new NullLogger(); });
            var actual = temp as NullLogger;
            Assert.IsNotNull(actual);
        }

        [Test]
        public void Debug_FormattedException()
        {
            TestFormattedException(_sut.Debug);
        }

        [Test]
        public void Debug_Exception()
        {
            TestException(_sut.Debug);
        }

        [Test]
        public void Trace_Exception()
        {
            TestException(_sut.Trace);
        }

        [Test]
        public void Info_Exception()
        {
            TestException(_sut.Info);
        }

        [Test]
        public void Warn_Exception()
        {
            TestException(_sut.Warn);
        }
        [Test]
        public void Error_Exception()
        {
            TestException(_sut.Error);
        }
        [Test]
        public void Fatal_Exception()
        {
            TestException(_sut.Fatal);
        }


        [Test]
        public void Debug_Formatted()
        {
            TestFormatted(_sut.Debug);
        }

        [Test]
        public void Trace_Formatted()
        {
            TestFormatted(_sut.Trace);
        }

        [Test]
        public void Info_Formatted()
        {
            TestFormatted(_sut.Info);
        }

        [Test]
        public void Warn_Formatted()
        {
            TestFormatted(_sut.Warn);
        }
        [Test]
        public void Error_Formatted()
        {
            TestFormatted(_sut.Error);
        }
        [Test]
        public void Fatal_Formatted()
        {
            TestFormatted(_sut.Fatal);
        }


        [Test]
        public void Debug_withMessage()
        {
            TestMessage(_sut.Debug);
        }

        [Test]
        public void Error_FormattedException()
        {
            TestFormattedException(_sut.Error);
        }

        [Test]
        public void Error_withMessage()
        {
            TestMessage(_sut.Error);
        }

        [Test]
        public void Fatal_FormattedException()
        {
            TestFormattedException(_sut.Fatal);
        }

        [Test]
        public void Fatal_withMessage()
        {
            TestMessage(_sut.Fatal);
        }

        [Test]
        public void Info_FormattedException()
        {
            TestFormattedException(_sut.Info);
        }

        [Test]
        public void Info_withMessage()
        {
            TestMessage(_sut.Info);
        }

        [Test]
        public void IsDebugEnabled()
        {
            Assert.IsFalse(_sut.IsDebugEnabled);
        }

        [Test]
        public void IsErrorEnabled()
        {
            Assert.IsFalse(_sut.IsErrorEnabled);
        }

        [Test]
        public void IsFatalEnabled()
        {
            Assert.IsFalse(_sut.IsFatalEnabled);
        }

        [Test]
        public void IsInfoEnabled()
        {
            Assert.IsFalse(_sut.IsInfoEnabled);
        }

        [Test]
        public void IsTraceEnabled()
        {
            Assert.IsFalse(_sut.IsTraceEnabled);
        }

        [Test]
        public void IsWarnEnabled()
        {
            Assert.IsFalse(_sut.IsWarnEnabled);
        }

        [Test]
        public void Name()
        {
            Assert.AreEqual(string.Empty, _sut.Name);
        }

        [Test]
        public void Trace_FormattedException()
        {
            TestFormattedException(_sut.Trace);
        }

        [Test]
        public void Trace_withMessage()
        {
            TestMessage(_sut.Trace);
        }

        [Test]
        public void Warn_FormattedException()
        {
            TestFormattedException(_sut.Warn);
        }

        [Test]
        public void Warn_withMessage()
        {
            TestMessage(_sut.Warn);
        }
    }
}