using System;
using System.Text;

namespace Cush.CommandLine.Internal
{
    [Serializable]
    internal struct OptionPair
    {
        private readonly string _option;
        private readonly string _parameters;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:OptionPair" /> structure with the specified option and parameters.
        /// </summary>
        /// <param name="option">The option.</param>
        /// <param name="parameters">The parameters associated with <paramref name="option" />.</param>
        public OptionPair(string option, string parameters)
        {
            _option = option;
            _parameters = parameters;
        }

        /// <summary>
        ///     Returns a string representation of the <see cref="T:OptionPair" />, using the string
        ///     representations of the option and parameters.
        /// </summary>
        /// <returns>
        ///     A string representation of the <see cref="T:OptionPair" />, which includes the
        ///     string representations of the option and parameters.
        /// </returns>
        public override string ToString()
        {
            var sb = new StringBuilder(16);
            sb.Append('[');
            if ((object) Option != null)
                sb.Append(Option);
            sb.Append(", ");
            if ((object) Parameters != null)
                sb.Append(Parameters);
            sb.Append(']');
            return sb.ToString();
        }

        /// <summary>
        ///     Gets the option.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:String" /> that represents the Option portion of the <see cref="T:OptionPair" />.
        /// </returns>
        public string Option
        {
            get { return _option; }
        }

        /// <summary>
        ///     Gets the parameters.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:String" /> that represents the Parameters portion of the <see cref="T:OptionPair" />.
        /// </returns>
        public string Parameters
        {
            get { return _parameters; }
        }
    }
}