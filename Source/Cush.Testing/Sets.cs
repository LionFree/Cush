namespace Cush.Testing
{
    public class Sets
    {
        /// <summary>
        ///     Upper- and lower-case English letters.
        /// </summary>
        public const string Alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        /// <summary>
        ///     Upper- and lower-case English letters, and the numbers 0 through 9.
        /// </summary>
        public const string AlphaNumeric = Alpha + "1234567890";

        /// <summary>
        ///     <para>The following characters:</para>
        ///     ! # $ % &amp; ' * + - / = ? ^ _ ` { | } ~
        /// </summary>
        public const string Symbols = "!#$%&'*+-/=?^_`{|}~";

        /// <summary>
        ///     Concatenation of the AlphaNumeric and Symbols sets.
        /// </summary>
        public const string AtomChars = AlphaNumeric + Symbols;
    }
}