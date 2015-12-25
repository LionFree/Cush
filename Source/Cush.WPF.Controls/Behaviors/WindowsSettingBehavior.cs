using System.Windows.Interactivity;

namespace Cush.WPF.Controls.Behaviors
{
    public class WindowsSettingBehaviour : Behavior<CushWindow>
    {
        protected override void OnAttached()
        {
            if (AssociatedObject != null && AssociatedObject.SaveWindowPosition)
            {
                // save with custom settings class or use the default way
                var windowPlacementSettings = this.AssociatedObject.WindowPlacementSettings ?? new WindowApplicationSettings(this.AssociatedObject);
                WindowSettings.SetSave(AssociatedObject, windowPlacementSettings);
            }
        }
    }
}