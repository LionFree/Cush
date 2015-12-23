using System;
using System.Diagnostics;
using Cush.CommandLine;
using Cush.Common.Interaction;

namespace Cush.TestHarness.ConsoleApp.Infrastructure
{
    internal class UserInteractionMessages : IDialogs<IConsole>
    {
        public void ShowError(string error)
        {
            Trace.WriteLine(error);
            Console.WriteLine(error);
        }

        public void ShowError(IConsole owner, string message)
        {
            throw new NotImplementedException();
        }

        public void ShowMessage(IConsole owner, string title, string message, object icon)
        {
            throw new NotImplementedException();
        }
    }
}