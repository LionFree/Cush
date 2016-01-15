using System.Diagnostics.CodeAnalysis;

namespace Cush.Common.Logging
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class Loggers
    {
        public static ILogger Null => new NullLogger();

        public static ILogger Trace => new TraceLogger();

        public static ILogger Console => new ConsoleLogger();

        public static ILogger File => new FileLogger();
    }
}