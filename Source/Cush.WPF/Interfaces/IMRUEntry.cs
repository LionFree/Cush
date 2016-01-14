namespace Cush.WPF.Interfaces
{
    public interface IMRUEntry
    {
        string Location { get; }

        /// <summary>
        ///     The full path of the file.  Includes the path, filename, and extension.
        /// </summary>
        string FullPath { get; set; }

        /// <summary>
        ///     The filename (without the path or extension).
        /// </summary>
        string FileName { get; }

        /// <summary>
        ///     The filename and extension (without the path).
        /// </summary>
        string FullFileName { get; }

        /// <summary>
        ///     The path where the file exists. (without the filename)
        /// </summary>
        string FilePath { get; }

        string ShortPath { get; set; }

        /// <summary>
        ///     The state of the entry; is it pinned to the top of the list?
        /// </summary>
        bool Pinned { get; set; }

        string ToString();
    }
}