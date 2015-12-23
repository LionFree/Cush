using System.Diagnostics;

namespace Cush.WPF.Debugging
{
    public class DebugTraceListener : TraceListener
    {
        public override void Write(string message)
        {
        }

        public override void WriteLine(string message)
        {
            Debug.WriteLine(message);
            Debugger.Break();
        }
    }
}