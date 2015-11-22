using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cush.CLI
{
    public interface ICommandLineParser
    {
        int Length { get; }
        string[] Keys { get; }
        string[] Values { get; }
        string this[string parameter] { get; }
        ICommandLineParser Parse(string[] args);

    }
}
