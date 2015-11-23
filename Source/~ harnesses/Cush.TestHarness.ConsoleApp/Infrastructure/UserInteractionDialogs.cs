using System;
using System.Diagnostics;
using Cush.Common.Interaction;

namespace Cush.TestHarness.ConsoleApp.Infrastructure
{
    internal class UserInteractionMessages : IDialogs
    {
        public void ShowError(string error)
        {
            Trace.WriteLine(error);
            Console.WriteLine(error);
        }

        public void ShowError<T>(T owner, string message)
        {
            throw new NotImplementedException();
        }

        public void ShowMessage<T>(T owner, string title, string message, object icon)
        {
            throw new NotImplementedException();
        }
    }
}