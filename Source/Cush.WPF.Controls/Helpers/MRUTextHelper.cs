using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace Cush.WPF.Controls.Helpers
{
    [DebuggerStepThrough]
    internal abstract class MRUTextHelper
    {
        internal static MRUTextHelper GetInstance()
        {
            return GetInstance(MRUHelperPrivates.GetInstance());
        }

        internal static MRUTextHelper GetInstance(MRUHelperPrivates helper)
        {
            return new MRUTextHelperImplementation(helper);
        }

        internal abstract string ShortenPath(string location, FontFamily fontFamily, FontStyle fontStyle,
            FontWeight fontWeight, FontStretch fontStretch, double fontSize,
            double boxWidth);

        #region Methods

        /// <summary>
        ///     Takes a string and returns the location of the last slash in the string.
        /// </summary>
        /// <param name="file">The string to strip.</param>
        /// <returns>The integer location of the last slash in the string.</returns>
        internal abstract int GetLastSlash(string file);

        #endregion

        private sealed class MRUTextHelperImplementation : MRUTextHelper
        {
            private readonly MRUHelperPrivates _helper;

            internal MRUTextHelperImplementation(MRUHelperPrivates helper)
            {
                _helper = helper;
            }

            internal override int GetLastSlash(string file)
            {
                var lastSlash = 0;

                for (int i = 1; i <= file.Length; i++)
                {
                    if (file.Substring(i - 1, 1) == "\\")
                    {
                        lastSlash = i;
                    }
                }
                return lastSlash;
            }

            internal override string ShortenPath(string location, FontFamily fontFamily, FontStyle fontStyle,
                FontWeight fontWeight,
                FontStretch fontStretch, double fontSize, double boxWidth)
            {
                string tempString = location;
                Size tempSize = _helper.MeasureText(tempString, fontFamily, fontStyle, fontWeight, fontStretch, fontSize);

                if (tempSize.Width > boxWidth)
                {
                    // Shorten the string until it's short enough
                    tempString = _helper.GetSubstringForWidth(tempString, boxWidth, fontFamily, fontStyle, fontWeight,
                        fontStretch, fontSize);
                }
                return tempString;
            }
        }
    }
}