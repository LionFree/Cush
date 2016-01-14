using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Cush.Common.FileHandling
{
    public static class MRUExtensions
    {
        /// <summary>
        /// Searches for the specified MRUEntry and returns the zero-based index of the first occurrence within the entire ObservableCollection&lt;MRUEntry&gt;.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="fullPath">The full path of the MRUEntry to locate in the ObservableCollection&lt;MRUEntry&gt;.</param>
        /// <returns></returns>
        public static int IndexOf(this IList<MRUEntry> collection, string fullPath)
        {
            // Set the return value to -1.
            // If we receive this value, then the fullPath doesn't exist in the collection.
            var returnValue = -1;

            // Iterate through the collection to find the fullPath.
            foreach (var file in collection)
            {
                if (file.FullPath == fullPath)
                {
                    returnValue = collection.IndexOf(file);
                }
            }

            // Return either the location of the item, or -1 if it isn't there.
            return returnValue;
        }

        public static void AddItem(this IList<MRUEntry> collection, MRUEntry item)
        {
            Trace.WriteLine("Adding item...");
            collection.Add(item);
        }
        /// <summary>
        /// Determines whether an element is in the ObservableCollection&lt;MRUEntry&gt;.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="fullpath">The full path of the MRUEntry to locate in the ObservableCollection&lt;MRUEntry&gt;.</param>
        /// <returns></returns>
        public static bool Contains(this IList<MRUEntry> collection, string fullpath)
        {
            bool result = false;

            foreach (MRUEntry item in collection)
            {
                if (item.FullPath == fullpath) result = true;
            }

            return result;
        }


    }
}
