using System;

namespace Cush.Common.Helpers
{
    public abstract class MRUEntryHelper
    {
        public static MRUEntryHelper GetInstance()
        {
            return new Implementation();
        }

        public abstract string[] ParseLocation(string filePath);
        public abstract string RemoveFileExtension(string fullPath);
        public abstract string GetFilenameWithExtension(string fullPath);
        public abstract string GetPathOnly(string fullPath);


        private class Implementation : MRUEntryHelper
        {
            public override string[] ParseLocation(string filePath)
            {
                if (filePath == null)
                {
                    throw new ArgumentNullException(nameof(filePath));
                }

                // Split the path into folders and filename.
                var temp = filePath.Split('\\');

                // Collect the folder names.
                var value = new string[temp.GetLength(0) - 1];
                Array.Copy(temp, value, temp.GetLength(0) - 1);

                // Return the folder names.
                return value;
            }

            public override string RemoveFileExtension(string fullPath)
            {
                var lastDot = 0;

                for (var i = 1; i <= fullPath.Length; i++)
                {
                    if (fullPath.Substring(i - 1, 1) == ".")
                    {
                        lastDot = i;
                    }
                }

                var output = fullPath;
                if (lastDot != 0)
                {
                    var lenFilename = lastDot - 1;
                    output = fullPath.Substring(0, lenFilename);
                }
                return output;
            }

            public override string GetFilenameWithExtension(string fullPath)
            {
                var lastSlash = GetLastSlash(fullPath);
                var lenFilename = fullPath.Length - lastSlash;
                return fullPath.Substring(fullPath.Length - lenFilename);
            }

            public override string GetPathOnly(string fullPath)
            {
                var lastSlash = GetLastSlash(fullPath);
                var lenFilename = fullPath.Length - lastSlash;
                return fullPath.Substring(0, fullPath.Length - lenFilename);
            }

            private int GetLastSlash(string fullPath)
            {
                var lastSlash = 0;

                for (var i = 1; i <= fullPath.Length; i++)
                {
                    if (fullPath.Substring(i - 1, 1) == "\\")
                    {
                        lastSlash = i;
                    }
                }
                return lastSlash;
            }
        }
    }
}