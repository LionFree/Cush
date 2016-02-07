namespace Cush.Common.FileHandling
{
    public class FileHandlerOptions
    {
        /// <summary>
        ///       Gets or sets the current file name filter string,
        ///       which determines the choices that appear in the "Save as file type" or
        ///       "Files of type" box at the bottom of the dialog box.
        ///
        ///       This is an example filter string:
        ///       Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*"
        /// </summary>
        /// <exception cref="System.ArgumentException">
        ///  Thrown in the setter if the new filter string does not have an even number of tokens
        ///  separated by the vertical bar character '|' (that is, the new filter string is invalid.)
        /// </exception>
        /// <remarks>
        ///  If DereferenceLinks is true and the filter string is null, a blank
        ///  filter string (equivalent to "|*.*") will be automatically substituted to work
        ///  around the issue documented in Knowledge Base article 831559
        ///     Callers must have FileIOPermission(PermissionState.Unrestricted) to call this API.
        /// </remarks>
        public string Filter { get; set; }

        /// <summary>
        /// The AddExtension property attempts to determine the appropriate extension
        /// by using the selected filter.  The DefaultExt property serves as a fallback - 
        ///  if the extension cannot be determined from the filter, DefaultExt will
        /// be used instead.
        /// </summary>
        public string DefaultExt { get; set; }

        /// <summary>
        /// Gets or sets an option flag indicating whether the 
        /// dialog box allows multiple files to be selected.
        /// </summary>
        /// <SecurityNote>
        ///     Critical: Dialog options are critical for set. 
        ///     PublicOk: Allowing MultiSelect is a safe operation
        /// </SecurityNote>
        public bool Multiselect { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the read-only 
        /// check box is selected.
        /// </summary>
        /// <SecurityNote>
        ///     Critical: Dialog options are critical for set. (Only critical for set
        ///             because setting options affects the behavior of the FileDialog)
        ///     PublicOk: ReadOnlyChecked is not a security critical option
        /// </SecurityNote>
        public bool ReadOnlyChecked { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog 
        /// contains a read-only check box.  
        /// </summary>
        /// <SecurityNote>
        ///     Critical: Dialog options are critical for set.
        ///     PublicOk: ShowReadOnly is not a security critical option
        /// </SecurityNote>
        public bool ShowReadOnly { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Save As dialog box displays a 
        /// warning if the user specifies a file name that already exists.
        /// </summary>
        /// <Remarks>
        ///     Callers must have UIPermission.AllWindows to call this API.
        /// </Remarks>
        /// <SecurityNote>
        ///     Critical: We do not want a Partially trusted application to have the ability
        ///                 to disable this prompt.
        ///     PublicOk: Demands UIPermission.AllWindows
        /// </SecurityNote>
        public bool OverwritePrompt { get; set; }
        
        /// <summary>
        ///  Gets or sets a string containing the full path of the file selected in 
        ///  the file dialog box.
        /// </summary>
        /// <Remarks>
        ///     Callers must have FileIOPermission(PermissionState.Unrestricted) to call this API.
        /// </Remarks>
        /// <SecurityNote> 
        ///     Critical: Do not want to allow access to raw paths to Parially Trusted Applications.
        ///     PublicOk: Demands FileIOPermission (PermissionState.Unrestricted)
        /// </SecurityNote>
        public string FileName { get; set; }
    }
}