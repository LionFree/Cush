using System.Windows;
using System.Windows.Interactivity;

namespace Cush.WPF.Controls.Behaviors
{
    public class StylizedBehaviorCollection : FreezableCollection<Behavior>
    {
        protected override Freezable CreateInstanceCore()
        {
            return new StylizedBehaviorCollection();
        }
    }
}