﻿using System.Windows;
using System.Windows.Interactivity;

namespace Cush.WPF.Controls.Actions
{
    public class SetFlyoutOpenAction : TargetedTriggerAction<FrameworkElement>
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(bool), typeof(SetFlyoutOpenAction), new PropertyMetadata(default(bool)));

        public bool Value
        {
            get { return (bool)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        protected override void Invoke(object parameter)
        {
            ((Flyout)TargetObject).IsOpen = Value;
        }
    }
}
