using System;
using System.IO;
using Cush.Common.Interaction;

namespace Cush.Windows.FileSystem
{
    public class PathShortener : IBreadcrumbShortener
    {
        private readonly BreadcrumbEllipsisFormat _options;
        private readonly IBreadcrumbCalculator _calculator;

        public PathShortener(BreadcrumbEllipsisFormat options, IBreadcrumbCalculator calculator)
        {
            _options = options;
            _calculator = calculator;
        }

        public string Compact(Type control, string path, double width)
        {
            var availableWidth = _calculator.StripPadding(control, width);
            if (_calculator.StringFitsWithinControl(control, path, availableWidth)) return path;
            
            // split path into <drive><directory><filename>
            var pre = Path.GetPathRoot(path);
            if (pre == null) throw new ArgumentException(Strings.EXCEPTION_PathRootIsNull, "path");

            var directory = Path.GetDirectoryName(path);
            if (directory == null) throw new ArgumentException(Strings.EXCEPTION_PathDirectoryIsNull, "path");

            var mid = directory.Substring(pre.Length);
            var post = Path.GetFileName(path);
            if (post == null) throw new ArgumentException(Strings.EXCEPTION_FilenameIsNull, "path");

            var len = 0;
            var seg = mid.Length;
            var fit = string.Empty;

            // find the longest string that fits into the
            // boundaries using the bisection method.
            while (seg > 1)
            {
                seg -= seg/2;
                var left = len + seg;
                var right = mid.Length;
                if (left > right) continue;

                if ((BreadcrumbEllipsisFormat.Middle & _options) ==
                    BreadcrumbEllipsisFormat.Middle)
                {
                    right -= left/2;
                    left -= left/2;
                }
                else if ((BreadcrumbEllipsisFormat.Start & _options) != 0)
                {
                    right -= left;
                    left = 0;
                }

                // build and measure a candidate string with ellipsis
                var txt = mid.Substring(0, left) + Strings.Noun_EllipsisChars + mid.Substring(right);

                // restore path with <drive> and <filename>
                txt = Path.Combine(Path.Combine(pre, txt), post);


                if (!_calculator.StringFitsWithinControl(control, txt, availableWidth)) continue;
                len += seg;
                fit = txt;
            }

            if (len != 0) return fit;
            
            // <drive> and <directory> are empty, return <filename>
            if (pre.Length == 0 && mid.Length == 0)
                return post;

            fit = Path.Combine(Path.Combine(pre, Strings.Noun_EllipsisChars), post);

            if (!_calculator.StringFitsWithinControl(control, fit, availableWidth))
                fit = Path.Combine(Strings.Noun_EllipsisChars, post);

            return fit;
        }
    }
}