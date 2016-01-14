using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Cush.WPF.Controls
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
    public class DialogSettings
    {
        public DialogSettings()
        {
            AffirmativeButtonText = "OK";
            NegativeButtonText = "Cancel";
            AnimateShow = true;
            AnimateHide = false;
            CancellationToken=CancellationToken.None;
        }

        /// <summary>
        /// Gets/sets the text used for the Affirmative button. For example: "OK" or "Yes".
        /// </summary>
        public string AffirmativeButtonText { get; set; }
        
        /// <summary>
        /// Gets/sets the text used for the Negative button. For example: "Cancel" or "No".
        /// </summary>
        public string NegativeButtonText { get; set; }

        /// <summary>
        ///     Enable/disable dialog showing animation.
        ///     "True" - play showing animation.
        ///     "False" - skip showing animation.
        /// </summary>
        public bool AnimateShow { get; set; }

        /// <summary>
        ///     Enable/disable dialog hiding animation
        ///     "True" - play hiding animation.
        ///     "False" - skip hiding animation.
        /// </summary>
        public bool AnimateHide { get; set; }

        /// <summary>
        /// Gets/sets the token to cancel the dialog.
        /// </summary>
        public CancellationToken CancellationToken { get; set; }

    }
}
