using System;
using System.Windows.Forms;
using Cush.Common.Interaction;

namespace Cush.TestHarness.WinForms
{
    internal class UserInteractionDialogs : IDialogs<Form>
    {
        public void ShowError(string error)
        {
            MessageBox.Show(error);
        }

        public void ShowError(Form owner, string message)
        {
            throw new NotImplementedException();
        }

        public void ShowMessage(Form owner, string title, string message, object icon)
        {
            throw new NotImplementedException();
        }
    }
}