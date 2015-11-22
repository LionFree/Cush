namespace Cush.Common.Interaction
{
    public interface IDialogs
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
        void ShowError<T>(T owner, string message);

        void ShowMessage<T>(T owner, string title, string message, object icon);
    }
}