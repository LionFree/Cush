using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Cush.CommandLine;
using Cush.Common;
using Cush.Windows;
using Cush.Windows.FileSystem;
using Cush.Windows.SingleInstance;

namespace Cush.TestHarness.ConsoleApp.Infrastructure
{
    [Serializable]
    internal abstract class Program : ISingleInstanceApplication
    {

        internal static Program ComposeObjectGraph()
        {
            return new ProgramImpl(new ConsoleProxy(), AssemblyProxy.Default);
        }

        private class ProgramImpl : Program
        {
            private readonly IConsole _console;
            private readonly IBuildInfo _buildInfo;

            public ProgramImpl(IConsole console, IBuildInfo buildInfo)
            {
                _console = console;
                _buildInfo = buildInfo;
            }

            public override bool OnSecondInstanceCreated(string[] args)
            {
                var newArgs = args.ToList();

                // The first argument is always the executable path.
                newArgs.RemoveAt(0);
                //return _commandLine.Process(newArgs.ToArray());

                var argString = newArgs.Aggregate(args[0], (current, arg) => current + (", " + arg));

                _console.SetOnTop();

                _console.WriteLine("Attempted to create second instance of application.  Command Line args received:\r\n" + argString);

                //_mainView.Activate();
                return true;
            }

            public override void Start(params string[] args)
            {
                try
                {
                    var env = new EnvironmentProxy();
                    var fs = new FileSystem();
                    var result = @"C:\Development\ServiceSentry\Design\Archive\20141221\ServiceSentry\Source\ServiceSentry.Model\Communication\IAgentService.cs";

                    var pathGetFileName = Path.GetFileName(result);
                    var getfilename = fs.GetResourceInfo(result).Name;

                    var location1 = Path.GetDirectoryName(result);
                    var location2 = fs.GetLocationInfo(result).Parent.FullName;

                    Trace.WriteLine($"{location1} ?= {location2}");


                    //const string special = "Special Notes!";
                    //const string appName = "The Pimpy App";
                    //var parser = new Parser(appName, "Test the command line.", true, _console, _buildInfo, special);
                    //parser.Parse(args);


                    //var bList = new BoundedList<string>(5);

                    //Test_ThrowHelper(null);
                    //Test_Benchmarks();

                    //Test_CompositionRoot(args);
                    //Test_ApplicationType();

                    _console.WriteLine();
                    _console.WriteLine("Press any key to exit...");
                    _console.ReadKey();
                }
                catch (Exception ex)
                {
                    // Clean up anything that might break/prevent the final cleanup
                    Trace.WriteLine(ex.Message);
                }
                finally
                {
                    // Clean up before closing application
                    Trace.WriteLine("Cleaning up Application in App.Main()");
                }
            }
        }

        public abstract bool OnSecondInstanceCreated(string[] args);

        public void InitializeAndRun(params string[] args)
        {
            Start(args);
        }

        public abstract void Start(params string[] args);
    }
}