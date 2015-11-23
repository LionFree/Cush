using System;
using System.Linq;
using System.Windows.Forms;
using Cush.Windows.Forms;
using Cush.Windows.SingleInstance;


namespace Cush.TestHarness.WinForms
{
    [Serializable]
    internal abstract class Program : ISingleInstanceApplication
    {
        public abstract bool OnSecondInstanceCreated(string[] args);
        public abstract void Start(params string[] args);

        internal static Program ComposeObjectGraph()
        {
            return new ProgramImpl();
        }

        [Serializable]
        private class ProgramImpl : Program
        {
            private Form1 _mainForm;

            public override bool OnSecondInstanceCreated(string[] args)
            {
                var newArgs = args.ToList();

                // The first argument is always the executable path.
                newArgs.RemoveAt(0);
                //return _commandLine.Process(newArgs.ToArray());

                var argString = newArgs.Aggregate(args[0], (current, arg) => current + (", " + arg));

                _mainForm.SetOnTop();
                
                MessageBox.Show("Attempted to create second instance of application.  Command Line args received:\r\n" +
                                argString);

                //_mainView.Activate();
                return true;
            }

            /// <summary>
            ///     The main entry point for the application.
            /// </summary>
            public override void Start(params string[] args)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                _mainForm = new Form1();
                Application.Run(_mainForm);
            }
        }
    }
}