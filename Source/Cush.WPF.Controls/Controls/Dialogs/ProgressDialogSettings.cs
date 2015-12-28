using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Cush.WPF.Controls
{
    [DebuggerStepThrough]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class ProgressDialogSettings : DialogSettings
    {
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public static readonly ProgressDialogSettings Cancellable = new ProgressDialogSettings(false, false, true);

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public static readonly ProgressDialogSettings NonCancellable = new ProgressDialogSettings(false, false, false);

        private ProgressDialogSettings(bool animateHide, bool animateShow, bool showCancelButton)
        {
            AnimateShow = animateShow;
            AnimateHide = animateHide;
            ShowCancelButton = showCancelButton;
        }

        /// <summary>
        ///     Enable/disable dialog showing the cancel button.
        ///     "True" - show the cancel button.
        ///     "False" - do not show the cancel button.
        /// </summary>
        public bool ShowCancelButton { get; set; }
    }
}