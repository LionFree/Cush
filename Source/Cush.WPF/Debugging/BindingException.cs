using System;

namespace Cush.WPF.Debugging
{
    /// <summary>
    /// Exception thrown when a WPF binding error occurs.
    /// </summary>
    [Serializable]
    public class BindingException : Exception
    {
        public BindingException(string message) : base(message)
        {

        }
    }
}
