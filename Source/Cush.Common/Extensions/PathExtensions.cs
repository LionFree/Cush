using System;
using System.IO;

namespace Cush.Common
{
    public static class PathExtensions
    {
        /// <summary>
        ///     If the supplied folder is valid, returns the foldername.
        ///     Otherwise, returns null.
        /// </summary>
        public static string ValidateFolderName(this string folderToValidate)
        {
            return ValidFolder(folderToValidate) ? folderToValidate : null;
        }

        /// <summary>
        /// <c>true</c> if the supplied folder is valid, otherwise <c>false</c>.
        /// </summary>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public static bool ValidFolder(string folderName)
        {
            if (folderName.IndexOfAny(Path.GetInvalidPathChars()) != -1) return false;

            try
            {
                var path = Path.GetFullPath(folderName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}