using System.Diagnostics.CodeAnalysis;

namespace Cush.Common.Logging
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class Loggers
    {
        public static ILogger Null
        {
            get { return new NullLogger(); }
        }

        public static ILogger Trace
        {
            get { return new TraceLogger(); }
        }

        public static ILogger Console
        {
            get { return new ConsoleLogger(); }
        }
    }
}