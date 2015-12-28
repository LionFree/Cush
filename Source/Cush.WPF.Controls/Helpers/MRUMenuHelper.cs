﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using Cush.Common;
using Cush.Common.Exceptions;
using Cush.Common.FileHandling;
using Cush.Common.Helpers;
using Path = System.IO.Path;

namespace Cush.WPF.Controls.Helpers
{
    [DebuggerStepThrough]
    internal abstract class MRUMenuHelper
    {
        internal static MRUMenuHelper GetInstance()
        {
            return GetInstance(MRUTextHelper.GetInstance(), MRUVisualHelper.GetInstance(), MRUEntryHelper.GetInstance());
        }

        internal static MRUMenuHelper GetInstance(MRUTextHelper textHelper, MRUVisualHelper mruVisualHelper,
                                                  MRUEntryHelper entryHelper)
        {
            return new MRUHelperImplementation(textHelper, mruVisualHelper, entryHelper);
        }


        internal abstract void UpdateSeparators(MRUFileMenu control);

        internal abstract SolidColorBrush AlterBrush(Brush brush1, float modifier);

        #region Methods

        internal abstract void ShortenListEntries(Control menu, IReadOnlyCollection<MRUEntry> fileList, string partName);
        internal abstract MRUEntry GetItemFromSelectionEvent(SelectionChangedEventArgs e);

        internal abstract void UpdateFileLists(ICollection<MRUEntry> files, IEnumerable<MRUEntry> pinnedList,
                                               IEnumerable<MRUEntry> unpinnedList);

        internal abstract void UpdateListboxStyle(ListBox listBox, Brush hotColor, Brush coldColor,
                                                  ICollection<ListBox> alreadyUpdated, bool force = false);

        #endregion

        private sealed class MRUHelperImplementation : MRUMenuHelper
        {
            private readonly MRUVisualHelper _mruVisualHelper;
            private readonly MRUTextHelper _textHelper;

            internal MRUHelperImplementation(MRUTextHelper textHelper, MRUVisualHelper mruVisualHelper,
                                             MRUEntryHelper entryHelper)
            {
                _textHelper = textHelper;
                _mruVisualHelper = mruVisualHelper;
            }

            internal override void UpdateSeparators(MRUFileMenu control)
            {
                // try to find the template
                var temp = control.Template;
                if (temp == null) return;

                var pinSeparator = control.Template.FindName("PART_PinnedSeparator", control) as Border;
                var unpinSeparator = control.Template.FindName("PART_UnpinnedSeparator", control) as Border;

                control.UpdateLayout();

                if (pinSeparator != null)
                {
                    pinSeparator.Visibility = control.PinnedFiles.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
                }

                if (unpinSeparator != null)
                {
                    unpinSeparator.Visibility = control.UnpinnedFiles.Count > 0
                                                    ? Visibility.Visible
                                                    : Visibility.Collapsed;
                }
            }

            internal override void UpdateFileLists(ICollection<MRUEntry> files, IEnumerable<MRUEntry> pinnedList,
                                                   IEnumerable<MRUEntry> unpinnedList)
            {
                AddFromList(files, pinnedList);
                AddFromList(files, unpinnedList);
            }

            private void AddFromList(ICollection<MRUEntry> files, IEnumerable<MRUEntry> list)
            {
                ThrowHelper.IfNullThenThrow(() => list);
                
                foreach (var file in list.Where(file => !files.Contains(file)))
                {
                    files.Add(file);
                }
            }

            internal override MRUEntry GetItemFromSelectionEvent(SelectionChangedEventArgs e)
            {
                MRUEntry item = null;

                if (e.AddedItems.Count != 0)
                {
                    item = (MRUEntry)e.AddedItems[0];
                }

                if (null == item)
                {
                    if (e.RemovedItems.Count != 0)
                        item = (MRUEntry)e.RemovedItems[0];
                }

                return item;
            }

            internal override void ShortenListEntries(Control menu, IReadOnlyCollection<MRUEntry> fileList,
                                                      string partName)
            {
                if (fileList.Count == 0) return;
                //if (Files.Count == 0) return;

                // use the styled settings as the default, in case everything else fails:
                //   boxwidth is 54 pixels smaller than the control.
                var boxWidth = menu.RenderSize.Width - 54;
                var fontSize = 9.0;
                var fontFamily = new FontFamily("SegoeUI");
                var fontStyle = FontStyles.Normal;
                var fontWeight = FontWeights.Normal;
                var fontStretch = FontStretches.Normal;

                // Find the listbox that is generated by the ControlTemplate of the Button
                var myListBox = (ListBox)menu.Template.FindName(partName, menu);

                if (null != myListBox)
                {
                    // Need to navigate down the visual tree because the CurrentItem may be null.

                    if (myListBox.HasItems)
                    {
                        var myListBoxItem =
                            (ListBoxItem)(myListBox.ItemContainerGenerator.ContainerFromItem(myListBox.Items[0]));

                        if (myListBoxItem != null)
                        {
                            // Getting the ContentPresenter of myListBoxItem
                            var myContentPresenter = TreeHelper.FindVisualChild<ContentPresenter>(myListBoxItem);

                            if (myContentPresenter != null)
                            {
                                // Finding textBlock from the DataTemplate that is set on that ContentPresenter
                                var myDataTemplate = myContentPresenter.ContentTemplate;
                                var myTextBlock = (TextBlock)myDataTemplate.FindName("PART_Path", myContentPresenter);

                                // Find the attributes of the textblock
                                boxWidth = myTextBlock.RenderSize.Width;
                                fontSize = myTextBlock.FontSize;
                                fontFamily = myTextBlock.FontFamily;
                                fontStyle = myTextBlock.FontStyle;
                                fontWeight = myTextBlock.FontWeight;
                                fontStretch = myTextBlock.FontStretch;
                            }
                            else
                            {
                                Trace.WriteLine("  ** " + partName + " has ListBoxItem, but can't get ContentPresenter.");
                            }
                        }
                        else
                        {
                            Trace.WriteLine("  ** " + partName + " has Items, but can't get ListBoxItem.");
                        }
                    }
                    else
                    {
                        Trace.WriteLine("  ** ListBox (" + partName + ") has no items.");
                    }
                }
                else
                {
                    Trace.WriteLine("** myListBox is null.");
                }


                // get string sizes.
                foreach (var item in fileList)
                {
                    item.ShortPath = _textHelper.ShortenPath(
                        item.Location, fontFamily, fontStyle, fontWeight, fontStretch, fontSize, boxWidth);
                }
            }

            internal override void UpdateListboxStyle(ListBox listBox,
                                                      Brush hotColor,
                                                      Brush coldColor,
                                                      ICollection<ListBox> alreadyUpdated,
                                                      bool force = false)
            {
                if (listBox == null) throw new ArgumentNullException("listBox");
                if (!force && alreadyUpdated.Contains(listBox)) return;

                var style = _mruVisualHelper.UpdateRectangleStyle(hotColor, coldColor);

                foreach (MRUEntry item in listBox.Items)
                {
                    var listBoxItem = (ListBoxItem)(listBox.ItemContainerGenerator.ContainerFromItem(item));
                    var contentPresenter = _mruVisualHelper.FindVisualChild<ContentPresenter>(listBoxItem);
                    var dataTemplate = contentPresenter.ContentTemplate;
                    var rectangle = (Rectangle)dataTemplate.FindName("PART_Pin", contentPresenter);
                    rectangle.Style = style;
                }

                if (!force) alreadyUpdated.Add(listBox);
            }

            internal override SolidColorBrush AlterBrush(Brush brush, float modifier)
            {
                if (brush == null) throw new ArgumentNullException(nameof(brush));
                var newBrush = brush as SolidColorBrush;
                if (newBrush == null) throw new ArgumentException(nameof(brush));

                var color = newBrush.Color;

                var red = (float)color.R;
                var green = (float)color.G;
                var blue = (float)color.B;

                if (modifier < 0)
                {
                    modifier = 1 + modifier;
                    red *= modifier;
                    green *= modifier;
                    blue *= modifier;
                }
                else
                {
                    red = (255 - red) * modifier + red;
                    green = (255 - green) * modifier + green;
                    blue = (255 - blue) * modifier + blue;
                }

                var alteredColor = Color.FromArgb(color.A, (byte)red, (byte)green, (byte)blue);

                return new SolidColorBrush(alteredColor);
            }
        }
    }

    [DebuggerStepThrough]
    internal abstract class MRUHelperPrivates
    {
        internal static MRUHelperPrivates GetInstance()
        {
            return new MRUHelperPrivateImplementation();
        }

        internal abstract Size MeasureText(string text, FontFamily fontFamily, FontStyle fontStyle,
                                           FontWeight fontWeight, FontStretch fontStretch, double fontSize);

        internal abstract string GetSubstringForWidth(string text, double width, FontFamily fontFamily,
                                                      FontStyle fontStyle, FontWeight fontWeight,
                                                      FontStretch fontStretch, double fontSize);

        #region Methods

        internal abstract Size MeasureTextSize(string text, FontFamily fontFamily, FontStyle fontStyle,
                                               FontWeight fontWeight, FontStretch fontStretch, double fontSize);

        /// <summary>
        ///     Attempts to shorten a path to a predetermined maximum length.
        /// </summary>
        /// <param name="pathname">The full path to shorten.</param>
        /// <param name="maxLength">The desired length.</param>
        /// <returns></returns>
        protected abstract string GetShortPathname(string pathname, int maxLength);

        #endregion

        private sealed class MRUHelperPrivateImplementation : MRUHelperPrivates
        {
            internal override Size MeasureTextSize(string text, FontFamily fontFamily, FontStyle fontStyle,
                                                   FontWeight fontWeight, FontStretch fontStretch, double fontSize)
            {
                var ft = new FormattedText(text,
                                           CultureInfo.CurrentCulture,
                                           FlowDirection.LeftToRight,
                                           new Typeface(fontFamily, fontStyle, fontWeight, fontStretch),
                                           fontSize,
                                           Brushes.Black);

                return new Size(ft.Width, ft.Height);
            }

            protected override string GetShortPathname(String pathname, int maxLength)
            {
                if (pathname.Length <= maxLength)
                    return pathname;

                var root = Path.GetPathRoot(pathname);
                if (root.Length > 3)
                    root += Path.DirectorySeparatorChar;

                var elements = pathname.Substring(root.Length)
                                       .Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

                var filenameIndex = elements.GetLength(0) - 1;

                if (elements.GetLength(0) == 1)
                {
                    // pathname is just a root and filename

                    if (elements[0].Length > 5)
                    {
                        //It's long enough to shorten
                        // If path is a UNC path, root may be
                        // rather long.
                        if (root.Length + 6 >= maxLength)
                        {
                            return root + elements[0].Substring(0, 3) + "...";
                        }

                        return pathname.Substring(0, maxLength - 3) + "...";
                    }
                }
                else if ((root.Length + 4 + elements[filenameIndex].Length) > maxLength)
                {
                    // pathname is just a root and filename
                    root += "...\\";

                    int len = elements[filenameIndex].Length;

                    if (len < 6)
                        return root + elements[filenameIndex];

                    if ((root.Length + 6) >= maxLength)
                    {
                        len = 3;
                    }
                    else
                    {
                        len = maxLength - root.Length - 3;
                    }
                    return root + elements[filenameIndex].Substring(0, len) + "...";
                }
                else if (elements.GetLength(0) == 2)
                {
                    return root + "...\\" + elements[1];
                }
                else
                {
                    // cut some elements out.
                    int begin = 0;
                    int len = elements[0].Length;

                    int totalLength = pathname.Length - len + 3;
                    int end = begin + 1;

                    while (totalLength > maxLength)
                    {
                        if (begin > 0)
                            totalLength -= elements[--begin].Length - 1;

                        if (totalLength <= maxLength)
                            break;

                        if (end < filenameIndex)
                            totalLength -= elements[++end].Length - 1;

                        if (begin == 0 && end == filenameIndex)
                            break;
                    }

                    // assemble final string.

                    for (int i = 0; i < begin; i++)
                    {
                        root += elements[i] + "\\";
                    }

                    root += "...\\";

                    for (int i = end; i < filenameIndex; i++)
                    {
                        root += elements[i] + "\\";
                    }
                    return root + elements[filenameIndex];
                }
                return pathname;
            }

            internal override Size MeasureText(string text, FontFamily fontFamily, FontStyle fontStyle,
                                               FontWeight fontWeight,
                                               FontStretch fontStretch, double fontSize)
            {
                var typeface = new Typeface(fontFamily, fontStyle, fontWeight, fontStretch);
                GlyphTypeface glyphTypeface;

                if (!typeface.TryGetGlyphTypeface(out glyphTypeface))
                {
                    return MeasureTextSize(text, fontFamily, fontStyle, fontWeight, fontStretch, fontSize);
                }

                double totalWidth = 0;
                double height = 0;

                foreach (char t in text)
                {
                    var glyphIndex = glyphTypeface.CharacterToGlyphMap[t];
                    var width = glyphTypeface.AdvanceWidths[glyphIndex] * fontSize;
                    var glyphHeight = glyphTypeface.AdvanceHeights[glyphIndex] * fontSize;

                    if (glyphHeight > height)
                    {
                        height = glyphHeight;
                    }
                    totalWidth += width;
                }
                return new Size(totalWidth, height);
            }


            internal override string GetSubstringForWidth(string text, double width, FontFamily fontFamily,
                                                          FontStyle fontStyle,
                                                          FontWeight fontWeight, FontStretch fontStretch,
                                                          double fontSize)
            {
                //Trace.WriteLine("  Path string too long... shortening.");
                if (width <= 0)
                {
                    return "";
                }

                int length = text.Length;
                string testString = text;

                while (true)
                {
                    var ft = new FormattedText(testString,
                                               CultureInfo.CurrentCulture,
                                               FlowDirection.LeftToRight,
                                               new Typeface(fontFamily, fontStyle, fontWeight, fontStretch),
                                               fontSize,
                                               Brushes.Black);
                    if (ft.Width <= width) break;

                    // remove a bit.
                    length--;
                    testString = text.Substring(0, length);
                }

                // Do we need an ellipsis?
                if (testString != text)
                {
                    // Strip off a character.
                    testString = testString.Substring(0, testString.Length - 3);

                    // Add an ellipsis.
                    testString += "…";
                }

                return testString;
            }
        }
    }

    [DebuggerStepThrough]
    internal abstract class MRUVisualHelper
    {
        internal static MRUVisualHelper GetInstance()
        {
            return GetInstance(MRUVisualHelperInternals.GetInstance());
        }

        internal static MRUVisualHelper GetInstance(MRUVisualHelperInternals helper)
        {
            return new MRUVisualHelperImplementation(helper);
        }

        internal abstract Style UpdateRectangleStyle(Brush hotColor, Brush coldColor);

        internal abstract TChildItem FindVisualChild<TChildItem>(DependencyObject obj)
            where TChildItem : DependencyObject;

        private sealed class MRUVisualHelperImplementation : MRUVisualHelper
        {
            private readonly MRUVisualHelperInternals _helper;

            private readonly Geometry _pinnedPathData = Geometry.Parse(
                "F1 M 22.7532,33.0046L 22.7532,29.004L 14.002,29.004C 14.3353,27.6705 14.6687,26.337 15.5022,25.6703C 16.2407,25.0794 17.3719,25.0121 18.5479,25.0045L 19.7528,17.0024C 18.7526,17.0024 18.0025,17.0024 17.3358,16.1689C 16.669,15.3355 16.3356,13.6686 16.0022,12.0017L 32.0045,12.0017C 31.6711,13.6686 31.3377,15.3355 30.6709,16.1689C 30.0042,17.0024 29.254,17.0024 28.2539,17.0024L 29.4588,25.0045C 30.6348,25.0121 31.766,25.0794 32.5045,25.6703C 33.338,26.337 33.6714,27.6705 34.0047,29.004L 25.2536,29.004L 25.2536,33.0046L 24.0033,39.2552L 22.7532,33.0046 Z ");

            private readonly Geometry _unpinnedPathData = Geometry.Parse(
                "F1 M 15.9971,22.7513L 19.9976,22.7512L 19.9976,14C 21.3311,14.3334 22.6647,14.6668 23.3314,15.5002C 23.9223,16.2388 23.9895,17.37 23.9972,18.5459L 31.9993,19.7508C 31.9993,18.7507 31.9993,18.0006 32.8327,17.3338C 33.6662,16.667 35.3331,16.3337 37,16.0003L 37,32.0025C 35.3331,31.6691 33.6662,31.3357 32.8327,30.669C 31.9993,30.0022 31.9993,29.2521 31.9993,28.2519L 23.9972,29.4569C 23.9895,30.6328 23.9223,31.764 23.3314,32.5026C 22.6647,33.336 21.3311,33.6694 19.9976,34.0028L 19.9976,25.2517L 15.9971,25.2517L 9.74651,24.0014L 15.9971,22.7513 Z ");

            internal MRUVisualHelperImplementation(MRUVisualHelperInternals helper)
            {
                _helper = helper;
            }


            internal override Style UpdateRectangleStyle(Brush hotColor, Brush coldColor)
            {
                var hotPinnedVisualBrush = _helper.CreateVisualBrush(_pinnedPathData, hotColor);
                var coldPinnedVisualBrush = _helper.CreateVisualBrush(_pinnedPathData, coldColor);
                var hotUnpinnedVisualBrush = _helper.CreateVisualBrush(_unpinnedPathData, hotColor);
                var coldUnpinnedVisualBrush = _helper.CreateVisualBrush(_unpinnedPathData,
                                                                        new SolidColorBrush(Colors.Transparent));

                var hotPinnedTrigger = _helper.CreateDataTrigger(true, true, hotPinnedVisualBrush);
                var coldPinnedTrigger = _helper.CreateDataTrigger(true, false, coldPinnedVisualBrush);
                var hotUnpinnedTrigger = _helper.CreateDataTrigger(false, true, hotUnpinnedVisualBrush);
                var coldUnpinnedTrigger = _helper.CreateDataTrigger(false, false, coldUnpinnedVisualBrush);

                var style = new Style(typeof(Rectangle));
                style.Triggers.Add(hotPinnedTrigger);
                style.Triggers.Add(hotUnpinnedTrigger);
                style.Triggers.Add(coldPinnedTrigger);
                style.Triggers.Add(coldUnpinnedTrigger);

                return style;
            }


            internal override TChildItem FindVisualChild<TChildItem>(DependencyObject obj)
            {
                for (var i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                {
                    var child = VisualTreeHelper.GetChild(obj, i);
                    if (child is TChildItem)
                        return (TChildItem)child;

                    var childOfChild = FindVisualChild<TChildItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
                return null;
            }
        }
    }

    internal abstract class MRUVisualHelperInternals
    {
        internal static MRUVisualHelperInternals GetInstance()
        {
            return new MRUVisualHelperInternalsImplementation();
        }

        internal abstract VisualBrush CreateVisualBrush(Geometry pathGeometry, Brush colorBrush);
        internal abstract MultiDataTrigger CreateDataTrigger(bool pinned, bool isMouseOver, VisualBrush visualBrush);

        private sealed class MRUVisualHelperInternalsImplementation : MRUVisualHelperInternals
        {
            internal override MultiDataTrigger CreateDataTrigger(bool pinned, bool isMouseOver, VisualBrush visualBrush)
            {
                var trigger = new MultiDataTrigger
                {
                    Conditions =
                            {
                                new Condition {Binding = new Binding("Pinned"), Value = pinned},
                                new Condition
                                    {
                                        Binding =
                                            new Binding("IsMouseOver")
                                                {
                                                    RelativeSource =
                                                        new RelativeSource(RelativeSourceMode.FindAncestor,
                                                                           typeof (DockPanel), 1)
                                                },
                                        Value = isMouseOver
                                    },
                            },
                    Setters =
                            {
                                new Setter(Shape.FillProperty, visualBrush),
                                new Setter(FrameworkElement.WidthProperty, pinned ? 9.0 : 15.0),
                                new Setter(FrameworkElement.HeightProperty, pinned ? 15.0 : 9.0)
                            }
                };

                return trigger;
            }

            internal override VisualBrush CreateVisualBrush(Geometry pathGeometry, Brush colorBrush)
            {
                var canvas = new Canvas();

                var path = new System.Windows.Shapes.Path
                {
                    Stretch = Stretch.Fill,
                    Width = 20.028,
                    Height = 27.2535,
                    Fill = colorBrush,
                    Data = pathGeometry
                };
                Canvas.SetLeft(path, 14.002);
                Canvas.SetTop(path, 12.0017);

                canvas.Children.Add(path);
                return new VisualBrush { Stretch = Stretch.Fill, Visual = canvas };
            }
        }
    }
}