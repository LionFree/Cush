/* 
 * This has been inpired by  
 * http://tech.pro/tutorial/940/wpf-snippet-detecting-binding-errors
 */

using System;
using System.Diagnostics;
using System.Text;

namespace Cush.WPF.Debugging
{
    /// <summary>
    /// A TraceListener that raise an event each time a trace is written
    /// </summary>
    internal sealed class ObservableTraceListener : TraceListener
    {
        private readonly StringBuilder _buffer = new StringBuilder();

        public override void Write(string message)
        {
            _buffer.Append(message);
        }

        [DebuggerStepThrough]
        public override void WriteLine(string message)
        {
            _buffer.Append(message);

            if (TraceCaught != null)
            {
                TraceCaught(_buffer.ToString());
            }

            _buffer.Clear();
        }

        public event Action<string> TraceCaught;
    }
}
