using System.Collections.Specialized;

namespace Cush.CLI
{
    public class CommandLineParser : ICommandLineParser
    {
        private readonly StringDictionary _parameters;

        public CommandLineParser()
        {
            _parameters = new StringDictionary();
        }

        public int Length { get { return (_parameters.Count); } }
        public string[] Keys
        {
            get
            {
                var output = new string[_parameters.Count];
                _parameters.Keys.CopyTo(output, 0);
                return output;
            }
        }
        public string[] Values
        {
            get
            {
                var output = new string[_parameters.Count];
                _parameters.Values.CopyTo(output, 0);
                return output;
            }
        }

        public string this[string parameter]
        {
            get { return (_parameters[parameter]); }
        }

        public ICommandLineParser Parse(string[] args)
        {
            // Valid format:
            // {switchCharacter}{switch}{delimiter}[quote]{parameter}[quote]

            if (_parameters.Count != 0) _parameters.Clear();

            foreach (var text in args)
            {
                var arg = CommandLineArgument.GetInstance(text);
                _parameters.Add(arg.Switch, arg.HasParameter ? arg.Parameter : true.ToString());
            }

            return this;
        }
    }
}