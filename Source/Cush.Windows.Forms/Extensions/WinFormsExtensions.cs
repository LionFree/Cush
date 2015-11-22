using System.Windows.Forms;
using Cush.Common.Exceptions;


namespace Cush.Windows.Forms
{
    public static class WinFormsExtensions
    {
        /// <summary>
        ///     Brings the form to the front of the z-order,
        ///     even if the form is a child of another application.
        /// </summary>
        public static Form SetOnTop(this Form form)
        {
            ThrowHelper.IfNullThenThrow(() => form);

            if (form.InvokeRequired)
            {
                form.Invoke(new MethodInvoker(delegate
                {
                    if (form.WindowState == FormWindowState.Minimized)
                        form.WindowState = FormWindowState.Normal;
                    form.Activate();
                }));
            }
            return form;
        }
    }
}