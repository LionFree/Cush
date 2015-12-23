using System;

namespace Cush.Common
{
    public static class SafeRaise
    {
        public delegate T GetEventArgs<out T>() where T : EventArgs;

        public static void Raise(EventHandler eventToRaise, object sender)
        {
            eventToRaise?.Invoke(sender, System.EventArgs.Empty);
        }

        public static void Raise(EventHandler<System.EventArgs> eventToRaise, object sender)
        {
            Raise(eventToRaise, sender, System.EventArgs.Empty);
        }

        public static void Raise<T>(EventHandler<T> eventToRaise, object sender, T args) where T : EventArgs
        {
            eventToRaise?.Invoke(sender, args);
        }

        public static void Raise<T>(EventHandler<T> eventToRaise, object sender, GetEventArgs<T> getEventArgs)
            where T : System.EventArgs
        {
            eventToRaise?.Invoke(sender, getEventArgs());
        }
    }
}