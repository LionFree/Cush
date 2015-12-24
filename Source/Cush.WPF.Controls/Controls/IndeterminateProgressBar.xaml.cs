using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Cush.WPF.Controls
{
    public partial class IndeterminateProgressBar
    {
        public static readonly DependencyProperty ProgressColorProperty =
            DependencyProperty.RegisterAttached("ProgressColor", typeof (Brush), typeof (IndeterminateProgressBar),
                new UIPropertyMetadata(null));

        public IndeterminateProgressBar()
        {
            InitializeComponent();
            DataContext = this;
            IsVisibleChanged += (s, e) => ((IndeterminateProgressBar) s).StartStopAnimation();
            var dpd = DependencyPropertyDescriptor.FromProperty(VisibilityProperty, GetType());
            dpd.AddValueChanged(this, (s, e) => ((IndeterminateProgressBar) s).StartStopAnimation());
        }

        public Brush ProgressColor
        {
            get { return (Brush) GetValue(ProgressColorProperty); }
            set { SetValue(ProgressColorProperty, value); }
        }

        private void StartStopAnimation()
        {
            var shouldAnimate = (Visibility == Visibility.Visible && IsVisible);
            Dispatcher.BeginInvoke(new Action(() =>
            {
                var s = Resources["animate"] as Storyboard;
                if (s != null)
                {
                    if (shouldAnimate)
                        s.Begin();
                    else
                        s.Stop();
                }
            }));
        }
    }
}