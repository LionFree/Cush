using System;
using System.Collections;
using System.IO;
using NUnit.Framework;
using Cush.Common.Exceptions;

namespace Cush.Exceptions.Tests.Exceptions
{
    [TestFixture]
    internal class ThrowHelperTests
    {
        [SetUp]
        public void SetUp()
        {
            _message = Path.GetRandomFileName();
            _argName = Path.GetRandomFileName();
            _expectedType = typeof (int);
        }

        private string _argName;
        private string _message;
        private Type _expectedType;

        [Test]
        public void IfNullAndNullsAreIllegalThenThrow_OK()
        {
            var tester = new ThrowTester<string>();
            Assert.DoesNotThrow(() => tester.Test_Name(null));
        }

        [Test]
        public void IfNullAndNullsAreIllegalThenThrow_ShouldThrow()
        {
            var tester = new ThrowTester<int>();
            Assert.Throws<ArgumentNullException>(() => tester.Test_Name(null));
        }

        [Test]
        public void IfNullAndNullsAreIllegalThenThrow_WithExpression_OK()
        {
            var tester = new ThrowTester<string>();
            var il = (IList) tester;
            Assert.DoesNotThrow(() => il.Add(null));
        }

        [Test]
        public void IfNullAndNullsAreIllegalThenThrow_WithExpression_ShouldThrow()
        {
            var tester = new ThrowTester<int>();
            var il = (IList) tester;
            Assert.Throws<ArgumentNullException>(() => il.Add(null));
        }

        [Test]
        public void IfNullOrEmptyThenThrow_UsingExpressionOnly_Empty()
        {
            var argName = string.Empty;
            try
            {
                ThrowHelper.IfNullOrEmptyThenThrow(() => argName);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(typeof(ArgumentException), ex.GetType());
                Assert.IsTrue(ex.Message.Contains("Parameter name: argName"));
            }
        }

        [Test]
        public void IfNullOrEmptyThenThrow_UsingExpressionOnly_OK()
        {
            Assert.DoesNotThrow(() => ThrowHelper.IfNullOrEmptyThenThrow(() => _argName));
        }



        [Test]
        public void IfNullOrEmptyThenThrow_UsingExpression_Empty()
        {
            var argName = string.Empty;
            try
            {
                ThrowHelper.IfNullOrEmptyThenThrow(() => argName, _message);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(typeof (ArgumentException), ex.GetType());
                Assert.IsTrue(ex.Message.Contains("Parameter name: argName"));
                Assert.IsTrue(ex.Message.Contains(_message));
            }
        }

        [Test]
        public void IfNullOrEmptyThenThrow_UsingExpression_Null()
        {
            string argName = null;
            try
            {
                ThrowHelper.IfNullOrEmptyThenThrow(() => argName, _message);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(typeof (ArgumentException), ex.GetType());
                Assert.IsTrue(ex.Message.Contains("Parameter name: argName"));
                Assert.IsTrue(ex.Message.Contains(_message));
            }
        }

        [Test]
        public void IfNullOrEmptyThenThrow_UsingExpression_OK()
        {
            Assert.DoesNotThrow(() => ThrowHelper.IfNullOrEmptyThenThrow(() => _argName, _message));
        }

        [Test]
        public void IfNullThenThrow_UsingExpression()
        {
            string argName = null;
            try
            {
                ThrowHelper.IfNullThenThrow(() => argName);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(typeof (ArgumentNullException), ex.GetType());
                Assert.IsTrue(ex.Message.Contains("Parameter name: argName"));
                //Assert.IsTrue(ex.Message.Contains(_message));
            }
        }

        [Test]
        public void IfNullThenThrow_UsingExpression_NotNull()
        {
            Assert.DoesNotThrow(() => ThrowHelper.IfNullThenThrow(() => _argName));
        }

        [Test]
        public void ThrowArgumentException_UsingExpressionAndMessage()
        {
            try
            {
                ThrowHelper.ThrowArgumentException(() => _argName, _message);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(typeof (ArgumentException), ex.GetType());
                Assert.IsTrue(ex.Message.Contains("Parameter name: _argName"));
                Assert.IsTrue(ex.Message.Contains(_message));
            }
        }

        [Test]
        public void ThrowArgumentNullException_using_Expression()
        {
            try
            {
                ThrowHelper.ThrowArgumentNullException(() => _message);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(typeof (ArgumentNullException), ex.GetType());
                Assert.IsTrue(ex.Message.Contains("Parameter name: _message"));
            }
        }

        [Test]
        public void ThrowArgumentNullException_UsingString()
        {
            try
            {
                ThrowHelper.ThrowArgumentNullException("_argName");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(typeof (ArgumentNullException), ex.GetType());
                Assert.IsTrue(ex.Message.Contains("Parameter name: _argName"));
                //Assert.IsTrue(ex.Message.Contains(_message));
            }
        }

        [Test]
        public void ThrowArgumentOutOfRangeException_using_Expression()
        {
            try
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(() => _argName);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(typeof (ArgumentOutOfRangeException), ex.GetType());
                Assert.IsTrue(ex.Message.Contains("Parameter name: _argName"));
            }
        }

        [Test]
        public void ThrowArgumentOutOfRangeException_using_ExpressionndMessage()
        {
            try
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(() => _argName, _message);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(typeof (ArgumentOutOfRangeException), ex.GetType());
                Assert.IsTrue(ex.Message.Contains("Parameter name: _argName"));
                Assert.IsTrue(ex.Message.Contains(_message));
            }
        }

        [Test]
        public void ThrowArgumentOutOfRangeException_using_NameAndMessage()
        {
            try
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(_argName, _message);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(typeof (ArgumentOutOfRangeException), ex.GetType());
                Assert.IsTrue(ex.Message.Contains("Parameter name: " + _argName));
                Assert.IsTrue(ex.Message.Contains(_message));
            }
        }

        [Test]
        public void ThrowInvalidOperationException()
        {
            try
            {
                ThrowHelper.ThrowInvalidOperationException(_message);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(typeof (InvalidOperationException), ex.GetType());
                Assert.AreEqual(_message, ex.Message);
            }
        }

        [Test]
        public void ThrowWrongValueTypeArgumentException_using_Expression()
        {
            try
            {
                ThrowHelper.ThrowWrongValueTypeArgumentException(() => _message, _expectedType);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(typeof (ArgumentException), ex.GetType());
                Assert.IsTrue(ex.Message.Contains("The value \"" + _message + "\""));
                Assert.IsTrue(ex.Message.Contains("is not of type \"" + _expectedType + "\""));
            }
        }

        [Test]
        public void ThrowWrongValueTypeArgumentException_using_Strings()
        {
            try
            {
                ThrowHelper.ThrowWrongValueTypeArgumentException("_message", _message, _expectedType);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(typeof (ArgumentException), ex.GetType());
                Assert.IsTrue(ex.Message.Contains("The value \"" + _message + "\""));
                Assert.IsTrue(ex.Message.Contains("is not of type \"" + _expectedType + "\""));
            }
        }
    }
}