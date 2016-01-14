using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;

namespace Cush.WPF.Controls
{
    /// <summary>
    /// Originally from http://xamlcoder.com/blog/2010/11/04/creating-a-metro-ui-style-control/
    /// </summary>
    //[SuppressMessage("ReSharper", "UnusedMember.Global")]
    //[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class AnimatingContentControl : ContentControl
    {
        public static readonly DependencyProperty ReverseTransitionProperty = DependencyProperty.Register("ReverseTransition", typeof(bool), typeof(AnimatingContentControl), new FrameworkPropertyMetadata(false));

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public bool ReverseTransition
        {
            get { return (bool)GetValue(ReverseTransitionProperty); }
            set { SetValue(ReverseTransitionProperty, value); }
        }

        public static readonly DependencyProperty TransitionsEnabledProperty = DependencyProperty.Register("TransitionsEnabled", typeof(bool), typeof(AnimatingContentControl), new FrameworkPropertyMetadata(true));

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public bool TransitionsEnabled
        {
            get { return (bool)GetValue(TransitionsEnabledProperty); }
            set { SetValue(TransitionsEnabledProperty, value); }
        }

        static AnimatingContentControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnimatingContentControl), new FrameworkPropertyMetadata(typeof(AnimatingContentControl)));
        }

        public AnimatingContentControl()
        {
            //DefaultStyleKey = typeof(CushContentControl);

            Loaded += CushContentControlLoaded;
            Unloaded += CushContentControlUnloaded;

            IsVisibleChanged += CushContentControlIsVisibleChanged;
        }

        void CushContentControlIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (TransitionsEnabled)
            {
                if (!IsVisible)
                    VisualStateManager.GoToState(this, ReverseTransition ? "AfterUnLoadedReverse" : "AfterUnLoaded", false);
                else
                    VisualStateManager.GoToState(this, ReverseTransition ? "AfterLoadedReverse" : "AfterLoaded", true);
            }
        }

        private void CushContentControlUnloaded(object sender, RoutedEventArgs e)
        {
            if (TransitionsEnabled)
                VisualStateManager.GoToState(this, ReverseTransition ? "AfterUnLoadedReverse" : "AfterUnLoaded", false);
        }

        private void CushContentControlLoaded(object sender, RoutedEventArgs e)
        {
            if (TransitionsEnabled)
                VisualStateManager.GoToState(this, ReverseTransition ? "AfterLoadedReverse" : "AfterLoaded", true);
            else
            {
                var root = ((Grid)GetTemplateChild("root"));
                if (root==null) throw new Exception("CushContentControl 'root' is null.");
                root.Opacity = 1.0;
                var transform = ((System.Windows.Media.TranslateTransform)root.RenderTransform);
                if (transform.IsFrozen)
                {
                    var modifiedTransform = transform.Clone();
                    modifiedTransform.X = 0;
                    root.RenderTransform = modifiedTransform;
                }
                else
                {
                    transform.X = 0;
                }
            }
        }

        public void Reload()
        {
            if (!TransitionsEnabled) return;

            if (ReverseTransition)
            {
                VisualStateManager.GoToState(this, "BeforeLoaded", true);
                VisualStateManager.GoToState(this, "AfterUnLoadedReverse", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "BeforeLoaded", true);
                VisualStateManager.GoToState(this, "AfterLoaded", true);
            }

        }
    }
}