using System.Windows;

namespace Cush.WPF.Controls
{
    public class FlyoutStatusChangedRoutedEventArgs : RoutedEventArgs
    {
        internal FlyoutStatusChangedRoutedEventArgs(RoutedEvent rEvent, object source) : base(rEvent, source)
        { }

        public Flyout ChangedFlyout { get; internal set; }
    }
}