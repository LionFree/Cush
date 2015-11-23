using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Documents;

namespace Cush.WPF.Exceptions
{
    public partial class ExceptionViewer : IExceptionViewer
    {
        private readonly ExceptionViewerHelper _helper;
        private double _chromeWidth;

        public ExceptionViewer(IExceptionViewerViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();

            _helper = ExceptionViewerHelper.GetInstance(Tree.FontSize);
        }

        public void ShowException(Exception ex)
        {
            Tree.Items.Clear();
            _helper.BuildTree(Tree, ex);
            _helper.ShowCurrentItem(Tree, Viewer, WrapText.IsChecked);
            Show();
        }

        internal void ExceptionViewer_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged)
            {
                _helper.SetMaxColumnWidth(this, _chromeWidth);
            }
        }

        internal void TreeView_OnSelectedItemChanged(object sender,
                                                              RoutedPropertyChangedEventArgs<object> e)
        {
            _helper.ShowCurrentItem(Tree, Viewer, WrapText.IsChecked);
        }

        internal void WrapText_OnChecked(object sender, RoutedEventArgs e)
        {
            if (!IsInitialized) return;
            _helper.ShowCurrentItem(Tree, Viewer, WrapText.IsChecked);
        }

        internal void OnClosing(object sender, CancelEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        internal void CopyToClipboard_OnClick(object sender, RoutedEventArgs e)
        {
            // Build a FlowDocument with Inlines from all top-level tree items.
            var doc = _helper.GetExceptionDetails(Tree);

            // Now place the doc contents on the clipboard in both
            // rich text and plain text format.
            var range = new TextRange(doc.ContentStart, doc.ContentEnd);
            var data = new DataObject();

            using (Stream stream = new MemoryStream())
            {
                range.Save(stream, DataFormats.Rtf);
                data.SetData(DataFormats.Rtf, Encoding.UTF8.GetString((stream as MemoryStream).ToArray()));
            }

            data.SetData(DataFormats.StringFormat, range.Text);
            Clipboard.SetDataObject(data);

            // The Inlines that were being displayed are now in the temporary document we just built,
            // causing them to disappear from the viewer.  This puts them back.
            _helper.ShowCurrentItem(Tree, Viewer, WrapText.IsChecked);
        }

        internal void ExceptionViewer_OnLoaded(object sender, RoutedEventArgs e)
        {
            // The grid column used for the tree started with Width="Auto" so it is now exactly
            // wide enough to fit the longest exception (up to the MaxWidth set in XAML).
            // Changing the width to a fixed pixel value prevents it from changing if the user
            // resizes the window.

            TreeColumn.Width = new GridLength(TreeColumn.ActualWidth, GridUnitType.Pixel);
            _chromeWidth = ActualWidth - GridRoot.ActualWidth;
            _helper.SetMaxColumnWidth(this, _chromeWidth);
        }

        internal void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    //public abstract partial class ExceptionViewer
    //{
    //    public abstract void ShowException(Exception ex);

    //    //internal abstract void ExceptionViewer_OnSizeChanged(object sender, SizeChangedEventArgs e);
    //    //internal abstract void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e);
    //    //internal abstract void WrapText_OnChecked(object sender, RoutedEventArgs e);
    //    //internal abstract void CopyToClipboard_OnClick(object sender, RoutedEventArgs e);
    //    //internal abstract void ExceptionViewer_OnLoaded(object sender, RoutedEventArgs e);
    //    //internal abstract void OnClosing(object sender, CancelEventArgs e);
    //    //internal abstract void CloseButton_OnClick(object sender, RoutedEventArgs e);

        
    //}
}