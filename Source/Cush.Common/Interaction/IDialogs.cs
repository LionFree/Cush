namespace Cush.Common.Interaction
{
    public interface IDialogs<in TOwner>
    {
        /// <summary>
        ///     Displays an error dialog with a given message.
        /// </summary>
        /// <param name="message">The message to be displayed.</param>
        void ShowError(string message);

        /// <summary>
        ///     Displays an error dialog with a given message.
        /// </summary>
        /// <param name="owner">The visual parent of the dialog box.</param>
        /// <param name="message">The message to be displayed.</param>
        void ShowError(TOwner owner, string message);

        void ShowMessage(TOwner owner, string title, string message, object icon);
    }
}