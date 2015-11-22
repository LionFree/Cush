using System.Windows.Forms;
using Cush.Common;
using Cush.Common.Interaction;

namespace Cush.TestHarness.WinForms
{
    internal class UserInteractionDialogs : IDialogs
    {
        public void ShowError(string error)
        {
            MessageBox.Show(error);
        }

        public void ShowError<T>(T owner, string message)
        {
            throw new System.NotImplementedException();
        }

        public void ShowMessage<T>(T owner, string title, string message, object icon)
        {
            throw new System.NotImplementedException();
        }
    }
}