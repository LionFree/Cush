using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Cush.WPF.Exceptions
{
    internal abstract class ExceptionViewerHelper
    {
        internal static ExceptionViewerHelper GetInstance(double baseFontSize)
        {
            var medium = baseFontSize * 1.1;
            var large = baseFontSize * 1.2;
            return new ExceptionViewerHelperImplementation(LineAdder.Create(),
                                                           ExceptionAdder.GetInstance(medium, large),
                                                           baseFontSize,
                                                           large);
        }

        #region Abstract Members

        internal abstract double CalcNoWrapWidth(IEnumerable<Inline> inlines);

        internal abstract void ShowCurrentItem(TreeView view, FlowDocumentScrollViewer flowDocumentScrollViewer,
                                               bool? wrapText);

        /// <summary>
        ///     Sets the widths of the grid columns, to prevent the gridsplitter
        ///     from being dragged beyond the right edge of the window.
        /// </summary>
        /// <param name="exceptionViewer">
        ///     The <see cref="ExceptionViewer" /> on which to set the column widths.
        /// </param>
        /// <param name="chromeWidth">The width of the window chrome.</param>
        internal abstract void SetMaxColumnWidth(ExceptionViewer exceptionViewer, double chromeWidth);

        internal abstract FlowDocument GetExceptionDetails(TreeView treeview);

        /// <summary>
        ///     Builds the tree in the left pane.
        ///     Each TreeViewItem.Tag will contain a list of Inlines
        ///     to display in the right-hand pane when it is selected.
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="exception"></param>
        internal abstract void BuildTree(TreeView treeView, Exception exception);

        #endregion

        private sealed class ExceptionViewerHelperImplementation : ExceptionViewerHelper
        {
            private readonly ExceptionAdder _exceptionAdder;
            private readonly double _largeFontSize;
            private readonly LineAdder _lineAdder;
            private readonly double _smallFontSize;

            internal ExceptionViewerHelperImplementation(LineAdder lineAdder, ExceptionAdder exceptionAdder, double smallFontSize,
                                                         double largeFontSize)
            {
                _lineAdder = lineAdder;
                _smallFontSize = smallFontSize;
                _largeFontSize = largeFontSize;
                _exceptionAdder = exceptionAdder;
            }

            internal override double CalcNoWrapWidth(IEnumerable<Inline> inlines)
            {
                double pageWidth = 0;
                var tb = new TextBlock();
                var size = new Size(double.PositiveInfinity, double.PositiveInfinity);

                foreach (var inline in inlines)
                {
                    tb.Inlines.Clear();
                    tb.Inlines.Add(inline);
                    tb.Measure(size);

                    if (tb.DesiredSize.Width > pageWidth) pageWidth = tb.DesiredSize.Width;
                }

                return pageWidth;
            }

            internal override void SetMaxColumnWidth(ExceptionViewer exceptionViewer, double chromeWidth)
            {
                // This prevents a GridSplitter from being dragged beyond the right edge of the window.
                exceptionViewer.GridRoot.MaxWidth = exceptionViewer.Width - chromeWidth;
                exceptionViewer.TreeColumn.MaxWidth = exceptionViewer.GridRoot.MaxWidth -
                                                      exceptionViewer.TextColumn.MinWidth;
            }


            internal override void ShowCurrentItem(TreeView treeView,
                                                   FlowDocumentScrollViewer documentViewer,
                                                   bool? wrapText)
            {
                var item = treeView.SelectedItem as TreeViewItem;
                if (item == null) return;

                var inlines = item.Tag as List<Inline>;
                if (inlines == null) return;

                var document = new FlowDocument
                {
                    FontSize = _smallFontSize,
                    FontFamily = treeView.FontFamily,
                    TextAlignment = TextAlignment.Left,
                    Background = documentViewer.Background
                };

                if (wrapText == false)
                {
                    document.PageWidth = CalcNoWrapWidth(inlines) + 50;
                }

                var para = new Paragraph();
                para.Inlines.AddRange(inlines);
                document.Blocks.Add(para);

                documentViewer.Document = document;
            }

            internal override FlowDocument GetExceptionDetails(TreeView treeView)
            {
                // Build a FlowDocument with Inlines from all top-level tree items.
                var inlines = new List<Inline>();
                var doc = new FlowDocument();
                var para = new Paragraph();

                doc.FontSize = _smallFontSize;
                doc.FontFamily = treeView.FontFamily;
                doc.TextAlignment = TextAlignment.Left;

                foreach (TreeViewItem treeItem in treeView.Items)
                {
                    if (inlines.Any())
                    {
                        // Put a line of underscores between each exception.

                        inlines.Add(new LineBreak());
                        inlines.Add(new Run("____________________________________________________"));
                        inlines.Add(new LineBreak());
                    }

                    var inlineList = treeItem.Tag as List<Inline>;
                    if (inlineList == null) continue;
                    inlines.AddRange(inlineList);
                }

                para.Inlines.AddRange(inlines);
                doc.Blocks.Add(para);
                return doc;
            }

            internal override void BuildTree(TreeView treeView, Exception e)
            {
                // The first node in the tree contains the summary message and all the
                // nested exception messages.
                var inlines = new List<Inline>();
                var firstItem = new TreeViewItem { Header = Strings.Noun_AllMessages };
                treeView.Items.Add(firstItem);

                var inline = new Bold(new Run(Strings.ExceptionViewer_SummaryMessage)) { FontSize = _largeFontSize };
                inlines.Add(inline);


                // Now add top-level nodes for each exception while building
                // the contents of the first node.
                while (e != null)
                {
                    inlines.Add(new LineBreak());
                    inlines.Add(new LineBreak());
                    _lineAdder.AddLines(inlines, e.Message);

                    _exceptionAdder.AddException(e, treeView);
                    e = e.InnerException;
                }

                firstItem.Tag = inlines;
                firstItem.IsSelected = true;
            }
        }

        #region classes

        internal abstract class ExceptionAdder
        {
            internal static ExceptionAdder GetInstance(double mediumFontSize, double largeFontSize)
            {
                return new ExceptionAdderImplementation(PropertyAdder.Create(mediumFontSize),
                                                        Renderer.Create(), largeFontSize);
            }

            internal abstract void AddException(Exception exception, TreeView treeView);

            private sealed class ExceptionAdderImplementation : ExceptionAdder
            {
                private readonly double _largeFontSize;
                private readonly PropertyAdder _propertyAdder;
                private readonly Renderer _renderer;

                internal ExceptionAdderImplementation(PropertyAdder propertyAdder, Renderer renderer,
                                                      double largeFontSize)
                {
                    _propertyAdder = propertyAdder;
                    _renderer = renderer;
                    _largeFontSize = largeFontSize;
                }

                internal override void AddException(Exception exception, TreeView treeView)
                {
                    // Create a list of Inlines containing all the properties of the exception object.
                    // The three most important properties (message, type, and stack trace) go first.

                    var exceptionItem = new TreeViewItem();
                    var inlines = new List<Inline>();
                    var properties = exception.GetType().GetProperties();

                    var exceptionType = exception.GetType().ToString();

                    exceptionType = (exceptionType.Contains("`"))
                                        ? exception.GetBaseException().GetType().ToString()
                                        : exceptionType;

                    exceptionItem.Header = exceptionType;
                    exceptionItem.Tag = inlines;
                    treeView.Items.Add(exceptionItem);

                    Inline inline = new Bold(new Run(exception.GetType().ToString()));
                    inline.FontSize = _largeFontSize;
                    inlines.Add(inline);

                    _propertyAdder.AddProperty(inlines, Strings.Header_Message, exception.Message);
                    _propertyAdder.AddProperty(inlines, Strings.Header_StackTrace, exception.StackTrace);

                    foreach (var info in properties)
                    {
                        // Skip InnerException because it will get a whole
                        // top-level node of its own.

                        if (info.Name != "InnerException")
                        {
                            var value = info.GetValue(exception, null);

                            if (value != null)
                            {
                                if (value is string)
                                {
                                    if (string.IsNullOrEmpty(value as string)) continue;
                                }
                                else if (value is IDictionary)
                                {
                                    value = _renderer.RenderDictionary(value as IDictionary);
                                    if (string.IsNullOrEmpty(value as string)) continue;
                                }
                                else if (value is IEnumerable)
                                {
                                    value = _renderer.RenderEnumerable(value as IEnumerable);
                                    if (string.IsNullOrEmpty(value as string)) continue;
                                }

                                if (info.Name != "Message" &&
                                    info.Name != "StackTrace")
                                {
                                    // Add the property to list for the exceptionItem.
                                    _propertyAdder.AddProperty(inlines, info.Name, value);
                                }

                                // Create a TreeViewItem for the individual property.
                                var propertyItem = new TreeViewItem();
                                var propertyInlines = new List<Inline>();

                                propertyItem.Header = info.Name;
                                propertyItem.Tag = propertyInlines;
                                exceptionItem.Items.Add(propertyItem);
                                _propertyAdder.AddProperty(propertyInlines, info.Name, value);
                            }
                        }
                    }
                }
            }
        }

        internal abstract class LineAdder
        {
            public static LineAdder Create()
            {
                return new LineAdderImplementation();
            }

            internal abstract void AddLines(List<Inline> inlines, string str);

            private sealed class LineAdderImplementation : LineAdder
            {
                // Adds the string to the list of Inlines, substituting
                // LineBreaks for an newline chars found.
                internal override void AddLines(List<Inline> inlines, string str)
                {
                    var lines = str.Split('\n');

                    inlines.Add(new Run(lines[0].Trim('\r')));

                    foreach (var line in lines.Skip(1))
                    {
                        inlines.Add(new LineBreak());
                        inlines.Add(new Run(line.Trim('\r')));
                    }
                }
            }
        }

        internal abstract class PropertyAdder
        {
            public static PropertyAdder Create(double mediumFontSize)
            {
                return new PropertyAdderImplementation(LineAdder.Create(), mediumFontSize);
            }

            internal abstract void AddProperty(List<Inline> inlines, string propName, object propVal);

            private sealed class PropertyAdderImplementation : PropertyAdder
            {
                private readonly double _fontSize;
                private readonly LineAdder _lineAdder;

                internal PropertyAdderImplementation(LineAdder lineAdder, double mediumFontSize)
                {
                    if (lineAdder == null) throw new ArgumentNullException("lineAdder");

                    _lineAdder = lineAdder;
                    _fontSize = mediumFontSize;
                }

                internal override void AddProperty(List<Inline> inlines, string propName, object propVal)
                {
                    inlines.Add(new LineBreak());
                    inlines.Add(new LineBreak());
                    var inline = new Bold(new Run(propName)) { FontSize = _fontSize };
                    inlines.Add(inline);
                    inlines.Add(new LineBreak());

                    if (propVal is string)
                    {
                        // Might have embedded newlines.

                        _lineAdder.AddLines(inlines, propVal as string);
                    }
                    else
                    {
                        inlines.Add(new Run(propVal.ToString()));
                    }
                }
            }
        }

        internal abstract class Renderer
        {
            internal static Renderer Create()
            {
                return new RendererImplementation();
            }

            internal abstract string RenderDictionary(IDictionary data);
            internal abstract string RenderEnumerable(IEnumerable data);

            private sealed class RendererImplementation : Renderer
            {
                internal override string RenderEnumerable(IEnumerable data)
                {
                    var result = new StringBuilder();

                    foreach (var obj in data)
                    {
                        result.AppendFormat("{0}\n", obj);
                    }

                    if (result.Length > 0) result.Length = result.Length - 1;
                    return result.ToString();
                }


                internal override string RenderDictionary(IDictionary data)
                {
                    var result = new StringBuilder();

                    foreach (var key in data.Keys)
                    {
                        if (key != null && data[key] != null)
                        {
                            result.AppendLine(key + " = " + data[key]);
                        }
                    }

                    if (result.Length > 0) result.Length = result.Length - 1;
                    return result.ToString();
                }
            }
        }

        #endregion
    }
}