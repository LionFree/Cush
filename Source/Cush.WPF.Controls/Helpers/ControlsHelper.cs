﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Cush.WPF.Controls.Helpers
{
    /// <summary>
    /// A helper class that provides various controls.
    /// </summary>
    public static class ControlsHelper
    {
        public static readonly DependencyProperty DisabledVisualElementVisibilityProperty = DependencyProperty.RegisterAttached("DisabledVisualElementVisibility", typeof(Visibility), typeof(ControlsHelper), new FrameworkPropertyMetadata(Visibility.Visible, FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets the value to handle the visibility of the DisabledVisualElement in the template.
        /// </summary>
        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        [AttachedPropertyBrowsableForType(typeof(PasswordBox))]
        [AttachedPropertyBrowsableForType(typeof(RichTextBox))]
        //[AttachedPropertyBrowsableForType(typeof(NumericUpDown))]
        public static Visibility GetDisabledVisualElementVisibility(UIElement element)
        {
            return (Visibility)element.GetValue(DisabledVisualElementVisibilityProperty);
        }

        /// <summary>
        /// Sets the value to handle the visibility of the DisabledVisualElement in the template.
        /// </summary>
        public static void SetDisabledVisualElementVisibility(UIElement element, Visibility value)
        {
            element.SetValue(DisabledVisualElementVisibilityProperty, value);
        }

        /// <summary>
        /// The DependencyProperty for the CharacterCasing property.
        /// Controls whether or not content is converted to upper or lower case
        /// </summary>
        public static readonly DependencyProperty ContentCharacterCasingProperty =
            DependencyProperty.RegisterAttached(
                "ContentCharacterCasing",
                typeof(CharacterCasing),
                typeof(ControlsHelper),
                new FrameworkPropertyMetadata(CharacterCasing.Normal, FrameworkPropertyMetadataOptions.AffectsMeasure),
                new ValidateValueCallback(value => CharacterCasing.Normal <= (CharacterCasing)value && (CharacterCasing)value <= CharacterCasing.Upper));

        /// <summary>
        /// Gets the character casing of the control
        /// </summary>
        [AttachedPropertyBrowsableForType(typeof(ContentControl))]
        //[AttachedPropertyBrowsableForType(typeof(DropDownButton))]
        //[AttachedPropertyBrowsableForType(typeof(WindowCommands))]
        public static CharacterCasing GetContentCharacterCasing(UIElement element)
        {
            return (CharacterCasing)element.GetValue(ContentCharacterCasingProperty);
        }

        /// <summary>
        /// Sets the character casing of the control
        /// </summary>
        public static void SetContentCharacterCasing(UIElement element, CharacterCasing value)
        {
            element.SetValue(ContentCharacterCasingProperty, value);
        }

        public static readonly DependencyProperty HeaderFontSizeProperty =
            DependencyProperty.RegisterAttached("HeaderFontSize", typeof(double), typeof(ControlsHelper), new FrameworkPropertyMetadata(26.67, HeaderFontSizePropertyChangedCallback) { Inherits = true });

        private static void HeaderFontSizePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is double)
            {
                // close button only for MetroTabItem
                var tabItem = dependencyObject as CushTabItem;
                if (tabItem == null)
                {
                    return;
                }

                if (tabItem.CloseButton == null)
                {
                    tabItem.ApplyTemplate();
                }

                if (tabItem.CloseButton != null && tabItem.ContentSite != null)
                {
                    // punker76: i don't like this! i think this must be done with xaml.
                    var fontDpiSize = (double)e.NewValue;
                    var fontHeight = Math.Ceiling(fontDpiSize * tabItem.FontFamily.LineSpacing);
                    var newMargin = (Math.Round(fontHeight) / 2.8)
                                    - tabItem.Padding.Top - tabItem.Padding.Bottom
                                    - tabItem.ContentSite.Margin.Top - tabItem.ContentSite.Margin.Bottom;

                    var previousMargin = tabItem.CloseButton.Margin;
                    tabItem.NewButtonMargin = new Thickness(previousMargin.Left, newMargin, previousMargin.Right, previousMargin.Bottom);
                    tabItem.CloseButton.Margin = tabItem.NewButtonMargin;

                    tabItem.CloseButton.UpdateLayout();
                }
            }
        }

        [AttachedPropertyBrowsableForType(typeof(CushTabItem))]
        [AttachedPropertyBrowsableForType(typeof(TabItem))]
        [AttachedPropertyBrowsableForType(typeof(GroupBox))]
        public static double GetHeaderFontSize(UIElement element)
        {
            return (double)element.GetValue(HeaderFontSizeProperty);
        }

        public static void SetHeaderFontSize(UIElement element, double value)
        {
            element.SetValue(HeaderFontSizeProperty, value);
        }

        public static readonly DependencyProperty HeaderFontStretchProperty =
            DependencyProperty.RegisterAttached("HeaderFontStretch", typeof(FontStretch), typeof(ControlsHelper), new UIPropertyMetadata(FontStretches.Normal));

        //[AttachedPropertyBrowsableForType(typeof(CushTabItem))]
        [AttachedPropertyBrowsableForType(typeof(TabItem))]
        [AttachedPropertyBrowsableForType(typeof(GroupBox))]
        public static FontStretch GetHeaderFontStretch(UIElement element)
        {
            return (FontStretch)element.GetValue(HeaderFontStretchProperty);
        }

        public static void SetHeaderFontStretch(UIElement element, FontStretch value)
        {
            element.SetValue(HeaderFontStretchProperty, value);
        }

        public static readonly DependencyProperty HeaderFontWeightProperty =
            DependencyProperty.RegisterAttached("HeaderFontWeight", typeof(FontWeight), typeof(ControlsHelper), new UIPropertyMetadata(FontWeights.Normal));

        //[AttachedPropertyBrowsableForType(typeof(CushTabItem))]
        [AttachedPropertyBrowsableForType(typeof(TabItem))]
        [AttachedPropertyBrowsableForType(typeof(GroupBox))]
        public static FontWeight GetHeaderFontWeight(UIElement element)
        {
            return (FontWeight)element.GetValue(HeaderFontWeightProperty);
        }

        public static void SetHeaderFontWeight(UIElement element, FontWeight value)
        {
            element.SetValue(HeaderFontWeightProperty, value);
        }

        /// <summary>
        /// This property can be used to set the button width (PART_ClearText) of TextBox, PasswordBox, ComboBox
        /// For multiline TextBox, PasswordBox is this the fallback for the clear text button! so it must set manually!
        /// For normal TextBox, PasswordBox the width is the height. 
        /// </summary>
        public static readonly DependencyProperty ButtonWidthProperty =
            DependencyProperty.RegisterAttached("ButtonWidth", typeof(double), typeof(ControlsHelper),
                                                new FrameworkPropertyMetadata(22d, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.Inherits));

        public static double GetButtonWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(ButtonWidthProperty);
        }

        public static void SetButtonWidth(DependencyObject obj, double value)
        {
            obj.SetValue(ButtonWidthProperty, value);
        }

        public static readonly DependencyProperty FocusBorderBrushProperty = DependencyProperty.RegisterAttached("FocusBorderBrush", typeof(Brush), typeof(ControlsHelper), new FrameworkPropertyMetadata(Brushes.Transparent, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));
        public static readonly DependencyProperty MouseOverBorderBrushProperty = DependencyProperty.RegisterAttached("MouseOverBorderBrush", typeof(Brush), typeof(ControlsHelper), new FrameworkPropertyMetadata(Brushes.Transparent, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Sets the brush used to draw the focus border.
        /// </summary>
        public static void SetFocusBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(FocusBorderBrushProperty, value);
        }

        /// <summary>
        /// Gets the brush used to draw the focus border.
        /// </summary>
        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        [AttachedPropertyBrowsableForType(typeof(CheckBox))]
        [AttachedPropertyBrowsableForType(typeof(RadioButton))]
        [AttachedPropertyBrowsableForType(typeof(DatePicker))]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        public static Brush GetFocusBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(FocusBorderBrushProperty);
        }

        /// <summary>
        /// Sets the brush used to draw the mouse over brush.
        /// </summary>
        public static void SetMouseOverBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(MouseOverBorderBrushProperty, value);
        }

        /// <summary>
        /// Gets the brush used to draw the mouse over brush.
        /// </summary>
        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        [AttachedPropertyBrowsableForType(typeof(CheckBox))]
        [AttachedPropertyBrowsableForType(typeof(RadioButton))]
        [AttachedPropertyBrowsableForType(typeof(DatePicker))]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        //[AttachedPropertyBrowsableForType(typeof(Tile))]
        public static Brush GetMouseOverBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MouseOverBorderBrushProperty);
        }

        /// <summary>
        /// DependencyProperty for <see cref="CornerRadius" /> property.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty
            = DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(ControlsHelper),
                                                  new FrameworkPropertyMetadata(
                                                      new CornerRadius(),
                                                      FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary> 
        /// The CornerRadius property allows users to control the roundness of the button corners independently by 
        /// setting a radius value for each corner. Radius values that are too large are scaled so that they
        /// smoothly blend from corner to corner. (Can be used e.g. at MetroButton style)
        /// Description taken from original Microsoft description :-D
        /// </summary>
        public static CornerRadius GetCornerRadius(UIElement element)
        {
            return (CornerRadius)element.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(UIElement element, CornerRadius value)
        {
            element.SetValue(CornerRadiusProperty, value);
        }
    }
}
