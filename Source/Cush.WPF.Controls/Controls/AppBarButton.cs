using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Cush.WPF.Controls
{
    /// <summary>
    /// </summary>
    public class AppBarButton : Button, INotifyPropertyChanged
    {
        static AppBarButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (AppBarButton),
                new FrameworkPropertyMetadata(typeof (AppBarButton)));
        }

        #region Image Size

        private const double SmallImageSize = 34;
        private const double LargeImageSize = 45;
        private const double NoImageSize = 0;

        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.Register("ImageHeight", typeof (double), typeof (AppBarButton),
                new PropertyMetadata(SmallImageSize));

        /// <summary>
        ///     Gets the height of the image.
        /// </summary>
        public double ImageHeight
        {
            get
            {
                switch (ImageSize)
                {
                    case ImageSize.None:
                        return NoImageSize;

                    case ImageSize.Small:
                        return SmallImageSize;

                    case ImageSize.Large:
                        return LargeImageSize;
                }
                return (double) GetValue(ImageHeightProperty);
            }
            internal set { SetValue(ImageHeightProperty, value); }
        }


        public static readonly DependencyProperty ImageWidthProperty =
            DependencyProperty.Register("ImageWidth", typeof (double), typeof (AppBarButton),
                new PropertyMetadata(SmallImageSize));

        /// <summary>
        ///     Gets the width of the image.
        /// </summary>
        public double ImageWidth
        {
            get
            {
                switch (ImageSize)
                {
                    case ImageSize.None:
                        return NoImageSize;

                    case ImageSize.Small:
                        return SmallImageSize;

                    case ImageSize.Large:
                        return LargeImageSize;
                }
                return (double) GetValue(ImageWidthProperty);
            }
            internal set { SetValue(ImageWidthProperty, value); }
        }


        public static readonly DependencyProperty ImageSizeProperty =
            DependencyProperty.Register("ImageSize", typeof (ImageSize), typeof (AppBarButton),
                new PropertyMetadata(ImageSize.Small));

        /// <summary>
        ///     Gets or sets the size of the image within an <see cref="AppBarButton"/>.
        /// </summary>
        public ImageSize ImageSize
        {
            get { return ((ImageSize) GetValue(ImageSizeProperty)); }
            set { SetValue(ImageSizeProperty, value); }
        }

        #endregion

        #region Text

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof (string), typeof (AppBarButton), new PropertyMetadata(null));

        /// <summary>
        ///     Gets or sets the text contents of an <see cref="AppBarButton"/>.
        /// </summary>
        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set
            {
                if (value == Text) return;

                SetValue(TextProperty, value);
                SetValue(IsTextEmptyProperty, string.IsNullOrEmpty(value));

                NotifyPropertyChanged();
            }
        }


        public static readonly DependencyProperty IsTextEmptyProperty =
            DependencyProperty.Register("IsTextEmpty", typeof (bool), typeof (AppBarButton), new PropertyMetadata(false));

        /// <summary>
        ///     Boolean value that indicates whether the text is empty.
        /// </summary>
        public bool IsTextEmpty
        {
            get
            {
                return string.IsNullOrEmpty(Text);
                //return ((bool)GetValue(IsTextEmptyProperty)); 
            }
            set
            {
                SetValue(IsTextEmptyProperty, value);
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Visual

        //public static readonly DependencyProperty VisualProperty =
        //   DependencyProperty.Register("Visual", typeof(Visual), typeof(AppBarButton), new PropertyMetadata(default(Visual)));

        ///// <summary>
        ///// Gets or sets the Visual content of a Cush.UI.AppBarButton.
        ///// </summary>
        //public Visual Visual
        //{
        //    get { return (Visual)GetValue(VisualProperty); }
        //    set
        //    {
        //        if(this.Visual != value)
        //        {
        //            SetValue(VisualProperty, value);
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}

        #endregion

        #region Property Changing

        public event PropertyChangedEventHandler PropertyChanged;


        // This method is called by the Set accessor of each property. 
        // The CallerMemberName attribute that is applied to the optional propertyName 
        // parameter causes the property name of the caller to be substituted as an argument. 
        private void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }


    public enum ImageSize
    {
        Small,
        Large,
        None
    }
}