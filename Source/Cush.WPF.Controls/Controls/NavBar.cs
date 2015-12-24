using System.Windows;
using System.Windows.Controls;

namespace Cush.WPF.Controls
{
    /// <summary>
    /// </summary>
    public class NavBar : ListBox
    {
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof (Orientation), typeof (NavBar),
                new UIPropertyMetadata(Orientation.Horizontal));


        public static readonly DependencyProperty ItemSizeProperty =
            DependencyProperty.Register("ItemSize", typeof (ItemSize), typeof (NavBar),
                new UIPropertyMetadata(ItemSize.Large));

        static NavBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (NavBar), new FrameworkPropertyMetadata(typeof (NavBar)));
        }

        /// <summary>
        ///     Determines whether the listitems are presented vertically or horizontally
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation) GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        /// <summary>
        ///     Determines whether the listitems are large or small.
        /// </summary>
        public ItemSize ItemSize
        {
            get { return (ItemSize) GetValue(ItemSizeProperty); }
            set { SetValue(ItemSizeProperty, value); }
        }
    }
}