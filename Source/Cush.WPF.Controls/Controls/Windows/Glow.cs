using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Cush.WPF.Controls
{
    public class Glow : Control
    {
        public static readonly DependencyProperty GlowBrushProperty = DependencyProperty.Register("GlowBrush",
            typeof (Brush), typeof (Glow), new UIPropertyMetadata(Brushes.Transparent));

        public static readonly DependencyProperty NonActiveGlowBrushProperty =
            DependencyProperty.Register("NonActiveGlowBrush", typeof (Brush), typeof (Glow),
                new UIPropertyMetadata(Brushes.Transparent));

        public static readonly DependencyProperty IsGlowProperty = DependencyProperty.Register("IsGlow", typeof (bool),
            typeof (Glow), new UIPropertyMetadata(true));

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation",
            typeof (Orientation), typeof (Glow), new UIPropertyMetadata(Orientation.Vertical));

        public static readonly DependencyProperty DirectionProperty = DependencyProperty.Register("Direction",
            typeof (GlowDirection), typeof (Glow), new UIPropertyMetadata(GlowDirection.Top));


        static Glow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (Glow), new FrameworkPropertyMetadata(typeof (Glow)));
        }

        public Brush GlowBrush
        {
            get { return (Brush) GetValue(GlowBrushProperty); }
            set { SetValue(GlowBrushProperty, value); }
        }

        public Brush NonActiveGlowBrush
        {
            get { return (Brush) GetValue(NonActiveGlowBrushProperty); }
            set { SetValue(NonActiveGlowBrushProperty, value); }
        }

        public bool IsGlow
        {
            get { return (bool) GetValue(IsGlowProperty); }
            set { SetValue(IsGlowProperty, value); }
        }

        public Orientation Orientation
        {
            get { return (Orientation) GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public GlowDirection Direction
        {
            get { return (GlowDirection) GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }
    }
}