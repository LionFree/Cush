using System;

namespace Cush.Common.Interaction
{
    /// <summary>
    ///     Specifies identifiers to indicate the return value of a dialog box.
    /// </summary>
    [Serializable]
    public sealed class DialogResult
    {
        #region Instances
        
        /// <summary>
        ///     The dialog box return value is <c>Abort</c> (usually sent from a button labelled Abort).
        /// </summary>
        public static readonly DialogResult Abort = new DialogResult("Abort");

        /// <summary>
        ///     The dialog box return value is <c>Cancel</c> (usually sent from a button labelled Cancel).
        /// </summary>
        public static readonly DialogResult Cancel = new DialogResult("Cancel");

        /// <summary>
        ///     The dialog box return value is <c>Ignore</c> (usually sent from a button labelled Ignore).
        /// </summary>
        public static readonly DialogResult Ignore = new DialogResult("Ignore");

        /// <summary>
        ///     The dialog box return value is <c>No</c> (usually sent from a button labelled No).
        /// </summary>
        public static readonly DialogResult No = new DialogResult("No");

        /// <summary>
        ///     <c>Nothing</c> is returned from the dialog box.  This means that the modal dialog continues running.
        /// </summary>
        public static readonly DialogResult None = new DialogResult("None");

        /// <summary>
        ///     The dialog box return value is <c>OK</c> (usually sent from a button labelled OK).
        /// </summary>
        public static readonly DialogResult OK = new DialogResult("OK");

        /// <summary>
        ///     The dialog box return value is <c>Retry</c> (usually sent from a button labelled Retry).
        /// </summary>
        public static readonly DialogResult Retry = new DialogResult("Retry");

        /// <summary>
        ///     The dialog box return value is <c>Yes</c> (usually sent from a button labelled Yes).
        /// </summary>
        public static readonly DialogResult Yes = new DialogResult("Yes");
        #endregion

        private static readonly char[] EnumSeperatorCharArray = {','};
        
        private readonly string _name;

        private DialogResult(string name)
        {
            _name = name;
        }

        /// <summary>
        ///     Converts the string representation of the name of a DialogResult to an equivalent DialogResult object.
        ///     The return value indicates whether the conversion succeeded.
        /// </summary>
        public static bool? TryParse(string value, out DialogResult result)
        {
            return TryParse(value, false, out result);
        }

        /// <summary>
        ///     Converts the string representation of the name of a DialogResult to an equivalent DialogResult object.
        ///     A parameter specifies whether the operation is case-sensitive.
        ///     The return value indicates whether the conversion succeeded.
        /// </summary>
        public static bool? TryParse(string value, bool ignoreCase, out DialogResult result)
        {
            var parseResult = new EnumResult();

            var retValue = TryParseDialogResult(value, ignoreCase, ref parseResult);
            result = parseResult.ParsedEnum;
            return retValue;
        }
        
        /// <summary>
        ///     Converts the value of this instance to its equivalent string representation.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Converts the value of this instance to its equivalent string representation.
        ///     </para>
        /// </remarks>
        public override string ToString()
        {
            return _name;
        }

        private static bool TryParseDialogResult(string value, bool ignoreCase, ref EnumResult parseResult)
        {
            if (value == null)
            {
                return false;
            }

            value = value.Trim();
            if (value.Length == 0)
            {
                return false;
            }

            DialogResult result = null;

            var values = value.Split(EnumSeperatorCharArray);

            string[] enumNames = { "Abort", "Cancel", "Ignore", "No", "None", "OK", "Retry", "Yes" };
            DialogResult[] enumValues = { Abort, Cancel, Ignore, No, None, OK, Retry, Yes };

            for (var i = 0; i < values.Length; i++)
            {
                values[i] = values[i].Trim(); // We need to remove whitespace characters

                var success = false;

                for (var j = 0; j < enumNames.Length; j++)
                {
                    if (ignoreCase)
                    {
                        if (string.Compare(enumNames[j], values[i], StringComparison.OrdinalIgnoreCase) != 0)
                            continue;
                    }
                    else
                    {
                        if (!enumNames[j].Equals(values[i]))
                            continue;
                    }

                    result = enumValues[j];

                    success = true;
                    break;
                }

                if (!success)
                {
                    return false;
                }
            }

            try
            {
                parseResult.ParsedEnum = result;
                return true;
            }
            catch (Exception)
            {
                //parseResult.SetFailure(ex);
                return false;
            }
        }
        
        // This will store the result of the parsing.
        private struct EnumResult
        {
            internal DialogResult ParsedEnum;
        }
    }
}