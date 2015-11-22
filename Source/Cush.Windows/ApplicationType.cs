using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Cush.Windows
{
    /// <summary>
    ///     Provides instance methods for determining the type of application:
    ///     e.g., <see cref="Console" />, <see cref="WPF" />, or <see cref="WinForms" />.
    /// </summary>
    [ComVisible(true)]
    public sealed class ApplicationType
    {
        /// <summary>
        ///     Represents a Console application.
        /// </summary>
        public static ApplicationType Console
        {
            get { return new ApplicationType {Name = "Console"}; }
        }

        /// <summary>
        ///     Represents a WPF application.
        /// </summary>
        public static ApplicationType WPF
        {
            get { return new ApplicationType {Name = "WPF"}; }
        }

        /// <summary>
        ///     Represents a Windows Forms application.
        /// </summary>
        public static ApplicationType WinForms
        {
            get { return new ApplicationType {Name = "WinForms"}; }
        }

        /// <summary>
        ///     Represents an application type that cannot be determined.
        /// </summary>
        public static ApplicationType UndefinedType
        {
            get { return new ApplicationType {Name = "UndefinedType"}; }
        }

        /// <summary>
        ///     The name of the application type.
        /// </summary>
        public string Name { get; private set; }

        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        ///     Returns the type of the currently executing application:
        ///     <see cref="Console" />,
        ///     <see cref="WPF" />,
        ///     or <see cref="WinForms" />.
        ///     If the application type cannot be determined, returns <see cref="UndefinedType" />.
        /// </summary>
        [DebuggerNonUserCode]
        public static ApplicationType Current
        {
            get
            {
                if (IsConsoleApplication()) return Console;

                if (MethodInStackFrame("PresentationFramework", "System.Windows.Application", "Run"))
                {
                    return WPF;
                }

                if (MethodInStackFrame("mscorlib", "System.Threading.ExecutionContext", "Run"))
                {
                    return WinForms;
                }

                return UndefinedType;
            }
        }

        // The DebuggerNonUserCodeAttribute should prevent the 
        // first-chance IOException from appearing in the debug 
        // window.  See below for more info.
        [DebuggerNonUserCode]
        private static bool IsConsoleApplication()
        {
            try
            {
                // ReSharper disable once UnusedVariable
                var title = System.Console.Title;
                Debug.WriteLine("First Chance IOException is expected when application is NOT a console app.");
                return true;
            }
            catch (IOException)
            {
                // This exception would normally appear as a 
                // first-chance exception in the output/debug 
                // window.  
                // Since we're using the exception internally, 
                // we don't need (or even want) it to appear 
                // in the user's output/debug windows.
                // The DebuggerNonUserCodeAttribute prevents the
                // IDE from displaying the FCE to the user.
                return false;
            }
        }

        private static bool MethodInStackFrame(string assemblyName, string typeName,
            string methodName)
        {
            var t = new StackTrace();
            var frames = t.GetFrames();
            if (null == frames) return false;

            return (from sf in frames
                let type = sf.GetMethod().DeclaringType
                where null != type
                let strAssemblyName = type.Assembly.GetName().Name
                let strTypeName = type.FullName
                let strMethod = sf.GetMethod().Name
                where strAssemblyName == assemblyName &&
                      strTypeName == typeName &&
                      strMethod == methodName
                select strAssemblyName)
                .Any();
        }
    }
}