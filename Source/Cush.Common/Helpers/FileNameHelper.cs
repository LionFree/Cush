using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Cush.Common.Helpers
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class FileNameHelper
    {
        /// <summary>
        ///     Takes a full path and returns just the filename (with extension).
        /// </summary>
        /// <param name="file">The path to strip.</param>
        /// <returns>The string filename.</returns>
        public static string GetFileName(string file)
        {
            var lastSlash = 0;

            for (var i = 1; i <= file.Length; i++)
            {
                if (file.Substring(i - 1, 1) == "\\")
                {
                    lastSlash = i;
                }
            }

            var lenFilename = file.Length - lastSlash;
            var output = file.Substring(file.Length - lenFilename);
            return output;
        }

        public static string ShortenPathname(string pathname, int maxLength)
        {
            if (pathname.Length <= maxLength)
                return pathname;
            var str1 = Path.GetPathRoot(pathname);
            if (str1.Length > 3)
                str1 += (string) (object) Path.DirectorySeparatorChar;
            var strArray = pathname.Substring(str1.Length)
                .Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            var index1 = strArray.GetLength(0) - 1;
            if (strArray.GetLength(0) == 1)
            {
                if (strArray[0].Length <= 5)
                    return pathname;
                if (str1.Length + 6 >= maxLength)
                    return str1 + strArray[0].Substring(0, 3) + "...";
                return pathname.Substring(0, maxLength - 3) + "...";
            }
            if (str1.Length + 4 + strArray[index1].Length > maxLength)
            {
                var str2 = str1 + "...\\";
                if (strArray[index1].Length < 6)
                    return str2 + strArray[index1];
                var length = str2.Length + 6 < maxLength ? maxLength - str2.Length - 3 : 3;
                return str2 + strArray[index1].Substring(0, length) + "...";
            }
            if (strArray.GetLength(0) == 2)
                return str1 + "...\\" + strArray[1];
            var num1 = 0;
            var length1 = strArray[0].Length;
            var num2 = pathname.Length - length1 + 3;
            var num3 = num1 + 1;
            while (num2 > maxLength)
            {
                if (num1 > 0)
                    num2 -= strArray[--num1].Length - 1;
                if (num2 > maxLength)
                {
                    if (num3 < index1)
                        num2 -= strArray[++num3].Length - 1;
                    if (num1 == 0 && num3 == index1)
                        break;
                }
                else
                    break;
            }
            for (var index2 = 0; index2 < num1; ++index2)
                str1 = str1 + strArray[index2] + "\\";
            var str3 = str1 + "...\\";
            for (var index2 = num3; index2 < index1; ++index2)
                str3 = str3 + strArray[index2] + "\\";
            return str3 + strArray[index1];
        }

        public static string StripFileExtension(string file)
        {
            var num = 0;
            for (var index = 1; index <= file.Length; ++index)
            {
                if (file.Substring(index - 1, 1) == ".")
                    num = index;
            }
            var str = file;
            if (num != 0)
            {
                var length = num - 1;
                str = file.Substring(0, length);
            }
            return str;
        }

        public static string StripFileName(string file)
        {
            var num1 = 0;
            for (var index = 1; index <= file.Length; ++index)
            {
                if (file.Substring(index - 1, 1) == "\\")
                    num1 = index;
            }
            var num2 = file.Length - num1;
            return file.Substring(file.Length - num2);
        }
    }
}