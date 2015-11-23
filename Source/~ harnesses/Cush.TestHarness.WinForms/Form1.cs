using System.Diagnostics;
using System.Windows.Forms;
using Cush.Windows;

namespace Cush.TestHarness.WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var type = ApplicationType.Current;
            Trace.WriteLine(type);
        }
    }
}