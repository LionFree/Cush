﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Cush.WPF.Styles.Helpers
{
    /// <summary>
    ///     Password watermarking code from: http://prabu-guru.blogspot.com/2010/06/how-to-add-watermark-text-to-textbox.html
    /// </summary>
    public class TextboxHelper : DependencyObject
    {
        /// <summary>
        ///     The clear text button behavior property. It sets a click event to the button if the value is true.
        /// </summary>
        public static readonly DependencyProperty IsClearTextButtonBehaviorEnabledProperty =
            DependencyProperty.RegisterAttached("IsClearTextButtonBehaviorEnabled", typeof (bool),
                typeof (TextboxHelper), new FrameworkPropertyMetadata(false, IsClearTextButtonBehaviorEnabledChanged));

        public static readonly DependencyProperty ButtonContentProperty =
            DependencyProperty.RegisterAttached("ButtonContent", typeof (object), typeof (TextboxHelper),
                new FrameworkPropertyMetadata("r"));

        public static readonly DependencyProperty ButtonFontFamilyProperty =
            DependencyProperty.RegisterAttached("ButtonFontFamily", typeof (FontFamily), typeof (TextboxHelper),
                new FrameworkPropertyMetadata((new FontFamilyConverter()).ConvertFromString("Marlett")));

        public static readonly DependencyProperty UseFloatingWatermarkProperty =
            DependencyProperty.RegisterAttached("UseFloatingWatermark", typeof (bool), typeof (TextboxHelper),
                new FrameworkPropertyMetadata(false, ButtonCommandOrClearTextChanged));

        public static readonly DependencyProperty ButtonCommandParameterProperty =
            DependencyProperty.RegisterAttached("ButtonCommandParameter", typeof (object), typeof (TextboxHelper),
                new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty ButtonCommandProperty =
            DependencyProperty.RegisterAttached("ButtonCommand", typeof (ICommand), typeof (TextboxHelper),
                new FrameworkPropertyMetadata(null, ButtonCommandOrClearTextChanged));


        public static readonly DependencyProperty IsMonitoringProperty =
            DependencyProperty.RegisterAttached("IsMonitoring", typeof (bool), typeof (TextboxHelper),
                new UIPropertyMetadata(false, OnIsMonitoringChanged));

        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.RegisterAttached("Watermark",
            typeof (string), typeof (TextboxHelper), new UIPropertyMetadata(string.Empty));

        public static readonly DependencyProperty TextLengthProperty = DependencyProperty.RegisterAttached(
            "TextLength", typeof (int), typeof (TextboxHelper), new UIPropertyMetadata(0));

        public static readonly DependencyProperty ClearTextButtonProperty =
            DependencyProperty.RegisterAttached("ClearTextButton", typeof (bool), typeof (TextboxHelper),
                new FrameworkPropertyMetadata(false, ClearTextChanged));

        public static readonly DependencyProperty SelectAllOnFocusProperty =
            DependencyProperty.RegisterAttached("SelectAllOnFocus", typeof (bool), typeof (TextboxHelper),
                new FrameworkPropertyMetadata(false));

        public static readonly DependencyProperty HasTextProperty = DependencyProperty.RegisterAttached("HasText", typeof(bool), typeof(TextboxHelper), new FrameworkPropertyMetadata(false));

        /// <summary>
        /// Gets if the attached TextBox has text.
        /// </summary>
        public static bool GetHasText(DependencyObject obj)
        {
            return (bool)obj.GetValue(HasTextProperty);
        }

        public static void SetHasText(DependencyObject obj, bool value)
        {
            obj.SetValue(HasTextProperty, value);
        }

        private static void IsClearTextButtonBehaviorEnabledChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var button = d as Button;
            if (e.OldValue != e.NewValue && button != null)
            {
                button.Click -= ButtonClicked;
                if ((bool) e.NewValue)
                {
                    button.Click += ButtonClicked;
                }
            }
        }

        /// <summary>
        ///     Gets the clear text button behavior.
        /// </summary>
        [AttachedPropertyBrowsableForType(typeof (Button))]
        public static bool GetIsClearTextButtonBehaviorEnabled(Button d)
        {
            return (bool) d.GetValue(IsClearTextButtonBehaviorEnabledProperty);
        }

        /// <summary>
        ///     Sets the clear text button behavior.
        /// </summary>
        [AttachedPropertyBrowsableForType(typeof (Button))]
        public static void SetIsClearTextButtonBehaviorEnabled(Button obj, bool value)
        {
            obj.SetValue(IsClearTextButtonBehaviorEnabledProperty, value);
        }

        public static void ButtonClicked(object sender, RoutedEventArgs e)
        {
            var button = ((Button) sender);
            var parent = VisualTreeHelper.GetParent(button);
            while (!(parent is TextBox || parent is PasswordBox || parent is ComboBox))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            var command = GetButtonCommand(parent);
            if (command != null && command.CanExecute(parent))
            {
                var commandParameter = GetButtonCommandParameter(parent);

                command.Execute(commandParameter ?? parent);
            }

            if (GetClearTextButton(parent))
            {
                if (parent is TextBox)
                {
                    ((TextBox) parent).Clear();
                }
                else if (parent is PasswordBox)
                {
                    ((PasswordBox) parent).Clear();
                }
                else if (parent is ComboBox)
                {
                    if (((ComboBox) parent).IsEditable)
                    {
                        ((ComboBox) parent).Text = string.Empty;
                    }
                    ((ComboBox) parent).SelectedItem = null;
                }
            }
        }

        public static object GetButtonContent(DependencyObject d)
        {
            return d.GetValue(ButtonContentProperty);
        }

        public static void SetButtonContent(DependencyObject obj, object value)
        {
            obj.SetValue(ButtonContentProperty, value);
        }

        public static FontFamily GetButtonFontFamily(DependencyObject d)
        {
            return (FontFamily) d.GetValue(ButtonFontFamilyProperty);
        }

        public static void SetButtonFontFamily(DependencyObject obj, FontFamily value)
        {
            obj.SetValue(ButtonFontFamilyProperty, value);
        }

        public static bool GetUseFloatingWatermark(DependencyObject obj)
        {
            return (bool) obj.GetValue(UseFloatingWatermarkProperty);
        }

        public static void SetUseFloatingWatermark(DependencyObject obj, bool value)
        {
            obj.SetValue(UseFloatingWatermarkProperty, value);
        }

        public static object GetButtonCommandParameter(DependencyObject d)
        {
            return d.GetValue(ButtonCommandParameterProperty);
        }

        public static void SetButtonCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(ButtonCommandParameterProperty, value);
        }

        public static ICommand GetButtonCommand(DependencyObject d)
        {
            return (ICommand) d.GetValue(ButtonCommandProperty);
        }

        public static void SetButtonCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(ButtonCommandProperty, value);
        }

        private static void ButtonCommandOrClearTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textbox = d as TextBox;
            if (textbox != null)
            {
                // only one loaded event
                textbox.Loaded -= TextBoxLoaded;
                textbox.Loaded += TextBoxLoaded;
                if (textbox.IsLoaded)
                {
                    TextBoxLoaded(textbox, new RoutedEventArgs());
                }
            }
            var passbox = d as PasswordBox;
            if (passbox != null)
            {
                // only one loaded event
                passbox.Loaded -= PassBoxLoaded;
                passbox.Loaded += PassBoxLoaded;
                if (passbox.IsLoaded)
                {
                    PassBoxLoaded(passbox, new RoutedEventArgs());
                }
            }
            var combobox = d as ComboBox;
            if (combobox != null)
            {
                // only one loaded event
                combobox.Loaded -= ComboBoxLoaded;
                combobox.Loaded += ComboBoxLoaded;
                if (combobox.IsLoaded)
                {
                    ComboBoxLoaded(combobox, new RoutedEventArgs());
                }
            }
        }

        static void ComboBoxLoaded(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                comboBox.SetValue(HasTextProperty,
                    !string.IsNullOrWhiteSpace(comboBox.Text) || comboBox.SelectedItem != null);
            }
        }

        public static void SetSelectAllOnFocus(DependencyObject obj, bool value)
        {
            obj.SetValue(SelectAllOnFocusProperty, value);
        }

        public static bool GetSelectAllOnFocus(DependencyObject obj)
        {
            return (bool) obj.GetValue(SelectAllOnFocusProperty);
        }

        public static void SetIsMonitoring(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMonitoringProperty, value);
        }

        public static string GetWatermark(DependencyObject obj)
        {
            return (string) obj.GetValue(WatermarkProperty);
        }

        public static void SetWatermark(DependencyObject obj, string value)
        {
            obj.SetValue(WatermarkProperty, value);
        }

        private static void SetTextLength(DependencyObject obj, int value)
        {
            obj.SetValue(TextLengthProperty, value);
            obj.SetValue(HasTextProperty, value >= 1);
        }

        static void OnIsMonitoringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox)
            {
                var txtBox = d as TextBox;

                if ((bool) e.NewValue)
                {
                    txtBox.TextChanged += TextChanged;
                    txtBox.GotFocus += TextBoxGotFocus;
                }
                else
                {
                    txtBox.TextChanged -= TextChanged;
                    txtBox.GotFocus -= TextBoxGotFocus;
                }
            }
            else if (d is PasswordBox)
            {
                var passBox = d as PasswordBox;

                if ((bool) e.NewValue)
                {
                    passBox.PasswordChanged += PasswordChanged;
                    passBox.GotFocus += PasswordGotFocus;
                }
                else
                {
                    passBox.PasswordChanged -= PasswordChanged;
                    passBox.GotFocus -= PasswordGotFocus;
                }
            }
        }

        static void TextChanged(object sender, TextChangedEventArgs e)
        {
            var txtBox = sender as TextBox;
            if (txtBox == null)
                return;
            SetTextLength(txtBox, txtBox.Text.Length);
        }

        static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passBox = sender as PasswordBox;
            if (passBox == null)
                return;
            SetTextLength(passBox, passBox.Password.Length);
        }

        static void TextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            var txtBox = sender as TextBox;
            if (txtBox == null)
                return;
            if (GetSelectAllOnFocus(txtBox))
            {
                txtBox.Dispatcher.BeginInvoke((Action) (txtBox.SelectAll));
            }
        }

        static void PasswordGotFocus(object sender, RoutedEventArgs e)
        {
            var passBox = sender as PasswordBox;
            if (passBox == null)
                return;
            if (GetSelectAllOnFocus(passBox))
            {
                passBox.Dispatcher.BeginInvoke((Action) (passBox.SelectAll));
            }
        }

        public static bool GetClearTextButton(DependencyObject d)
        {
            return (bool) d.GetValue(ClearTextButtonProperty);
        }

        public static void SetClearTextButton(DependencyObject obj, bool value)
        {
            obj.SetValue(ClearTextButtonProperty, value);
        }

        private static void ClearTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textbox = d as TextBox;
            if (textbox != null)
            {
                if ((bool) e.NewValue)
                {
                    textbox.Loaded += TextBoxLoaded;
                }
                else
                {
                    textbox.Loaded -= TextBoxLoaded;
                }
            }
            var passbox = d as PasswordBox;
            if (passbox != null)
            {
                if ((bool) e.NewValue)
                {
                    passbox.Loaded += PassBoxLoaded;
                }
                else
                {
                    passbox.Loaded -= PassBoxLoaded;
                }
            }
        }

        static void PassBoxLoaded(object sender, RoutedEventArgs e)
        {
            if (!(sender is PasswordBox))
                return;

            var passbox = sender as PasswordBox;
            if (passbox.Style == null)
                return;

            var template = passbox.Template;
            if (template == null)
                return;

            var button = template.FindName("PART_ClearText", passbox) as Button;
            if (button == null)
                return;

            if (GetClearTextButton(passbox))
            {
                button.Click += ClearPassClicked;
            }
            else
            {
                button.Click -= ClearPassClicked;
            }
        }


        static void TextBoxLoaded(object sender, RoutedEventArgs e)
        {
            if (!(sender is TextBox))
                return;

            var textbox = sender as TextBox;
            if (textbox.Style == null)
                return;

            var template = textbox.Template;
            if (template == null)
                return;

            var button = template.FindName("PART_ClearText", textbox) as Button;
            if (button == null)
                return;

            if (GetClearTextButton(textbox))
            {
                button.Click += ClearTextClicked;
            }
            else
            {
                button.Click -= ClearTextClicked;
            }
        }

        static void ClearTextClicked(object sender, RoutedEventArgs e)
        {
            var button = ((Button) sender);
            var parent = VisualTreeHelper.GetParent(button);
            while (!(parent is TextBox))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            ((TextBox) parent).Clear();
        }

        static void ClearPassClicked(object sender, RoutedEventArgs e)
        {
            var button = ((Button) sender);
            var parent = VisualTreeHelper.GetParent(button);
            while (!(parent is PasswordBox))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            ((PasswordBox) parent).Clear();
        }
    }
}