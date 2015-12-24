using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;

namespace Cush.WPF.Controls
{
    public class ResizingPage : ContentControl
    {
        [SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
        public ResizingPage()
        {
        }

        static ResizingPage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (ResizingPage), new FrameworkPropertyMetadata(typeof (ResizingPage)));
        }

        /// <summary>
        ///     Tells the system what to do when the window orientation changes between Landscape and Portrait.
        /// </summary>
        /// <param name="orientation">
        ///     The System.Windows.Controls.Orientation corresponding to the current dimensions of the
        ///     application shell window.
        /// </param>
        public virtual void SetUIOrientation(Orientation orientation)
        {
        }

        /// <summary>
        ///     Handles the SizeChanged event of the application shell window.
        /// </summary>
        public virtual void page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var orientation = Orientation.Horizontal;

            //Console.WriteLine("Height: " + this.WindowHeight.ToString() + "     Width: " + this.WindowWidth.ToString());

            if (Height > Width) // Portrait
            {
                orientation = Orientation.Vertical;
            }

            SetUIOrientation(orientation);
        }
    }
}