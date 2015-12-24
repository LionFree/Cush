using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Cush.WPF.Controls.Behaviors
{
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public static class ReloadBehavior
    {
        public static DependencyProperty OnDataContextChangedProperty =
            DependencyProperty.RegisterAttached("OnDataContextChanged", typeof (bool), typeof (ReloadBehavior),
                new PropertyMetadata(OnDataContextChanged));

        public static DependencyProperty OnSelectedTabChangedProperty =
            DependencyProperty.RegisterAttached("OnSelectedTabChanged", typeof (bool), typeof (ReloadBehavior),
                new PropertyMetadata(OnSelectedTabChanged));

        public static readonly DependencyProperty AnimatingContentControlProperty =
            DependencyProperty.RegisterAttached("AnimatingContentControl", typeof (AnimatingContentControl),
                typeof (ReloadBehavior), new PropertyMetadata(default(AnimatingContentControl)));

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public static bool GetOnDataContextChanged(AnimatingContentControl element)
        {
            return (bool) element.GetValue(OnDataContextChangedProperty);
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public static void SetOnDataContextChanged(AnimatingContentControl element, bool value)
        {
            element.SetValue(OnDataContextChangedProperty, value);
        }

        private static void OnDataContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((AnimatingContentControl) d).DataContextChanged += ReloadDataContextChanged;
        }

        static void ReloadDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((AnimatingContentControl) sender).Reload();
        }

        public static bool GetOnSelectedTabChanged(AnimatingContentControl element)
        {
            return (bool) element.GetValue(OnDataContextChangedProperty);
        }

        public static void SetOnSelectedTabChanged(AnimatingContentControl element, bool value)
        {
            element.SetValue(OnDataContextChangedProperty, value);
        }

        private static void OnSelectedTabChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((AnimatingContentControl) d).Loaded += ReloadLoaded;
        }

        private static void ReloadLoaded(object sender, RoutedEventArgs e)
        {
            var animatingContentControl = ((AnimatingContentControl) sender);
            var tab = Ancestors(animatingContentControl)
                .OfType<TabControl>()
                .FirstOrDefault();

            if (tab == null) return;

            SetAnimatingContentControl(tab, animatingContentControl);
            tab.SelectionChanged += ReloadSelectionChanged;
        }

        private static IEnumerable<DependencyObject> Ancestors(DependencyObject obj)
        {
            var parent = VisualTreeHelper.GetParent(obj);
            while (parent != null)
            {
                yield return parent;
                obj = parent;
                parent = VisualTreeHelper.GetParent(obj);
            }
        }

        static void ReloadSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.OriginalSource != sender)
                return;

            GetAnimatingContentControl((TabControl) sender).Reload();
        }

        public static void SetAnimatingContentControl(UIElement element, AnimatingContentControl value)
        {
            element.SetValue(AnimatingContentControlProperty, value);
        }

        public static AnimatingContentControl GetAnimatingContentControl(UIElement element)
        {
            return (AnimatingContentControl) element.GetValue(AnimatingContentControlProperty);
        }
    }
}