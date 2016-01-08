using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Cush.WPF.Controls.Helpers
{
    [DebuggerStepThrough]
    internal abstract class MRUTextHelper
    {
        internal static MRUTextHelper GetInstance()
        {
            return GetInstance(new MRUHelperPrivates());
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

    [DebuggerStepThrough]
    internal class MRUHelperPrivates
    {
        private Size MeasureTextSize(string text, FontFamily fontFamily, FontStyle fontStyle,
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

        internal Size MeasureText(string text, FontFamily fontFamily, FontStyle fontStyle,
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

            foreach (var t in text)
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


        internal string GetSubstringForWidth(string text, double width, FontFamily fontFamily,
            FontStyle fontStyle,
            FontWeight fontWeight, FontStretch fontStretch,
            double fontSize)
        {
            //Trace.WriteLine("  Path string too long... shortening.");
            if (width <= 0)
            {
                return "";
            }

            var length = text.Length;
            var testString = text;

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