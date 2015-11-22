// -----------------------------------------------------------------------
//      Copyright (c) 2014 Curtis Kaler. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Cush.WPF.Controls
{
    /// <summary>
    ///     Interaction logic for AppBarButton.xaml
    /// </summary>
    public class AppBarButton : Button, INotifyPropertyChanged
    {
        static AppBarButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (AppBarButton),
                                                     new FrameworkPropertyMetadata(typeof (AppBarButton)));
        }

        #region Image Size

        internal const double SmallImageSize = 34;
        internal const double LargeImageSize = 45;
        internal const double NoImageSize = 0;

        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.Register("ImageHeight", typeof (double), typeof (AppBarButton),
                                        new PropertyMetadata(SmallImageSize));


        public static readonly DependencyProperty ImageWidthProperty =
            DependencyProperty.Register("ImageWidth", typeof (double), typeof (AppBarButton),
                                        new PropertyMetadata(SmallImageSize));


        public static readonly DependencyProperty ImageSizeProperty =
            DependencyProperty.Register("ImageSize", typeof (ImageSize), typeof (AppBarButton),
                                        new PropertyMetadata(ImageSize.Small));

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

        /// <summary>
        ///     Gets or sets the size of the image within a Bedouin.AppBarButton.
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


        public static readonly DependencyProperty IsTextEmptyProperty =
            DependencyProperty.Register("IsTextEmpty", typeof (bool), typeof (AppBarButton), new PropertyMetadata(false));

        /// <summary>
        ///     Gets or sets the text contents of a Bedouin.AppBarButton.
        /// </summary>
        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set
            {
                if (value != Text)
                {
                    SetValue(TextProperty, value);
                    SetValue(IsTextEmptyProperty, string.IsNullOrEmpty(value));

                    NotifyPropertyChanged();
                }
            }
        }

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
        ///// Gets or sets the Visual content of a Bedouin.AppBarButton.
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
        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
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