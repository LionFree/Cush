using System;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes
{
    public class ColorSchemeChangedEventArgs : EventArgs
    {
        private readonly IColorScheme _newScheme;
        private readonly IColorScheme _oldScheme;

        public ColorSchemeChangedEventArgs(IColorScheme oldScheme, IColorScheme newScheme)
        {
            _newScheme = newScheme;
            _oldScheme = oldScheme;
        }

        public IColorScheme NewScheme
        {
            get { return _newScheme; }
        }

        public IColorScheme OldScheme
        {
            get { return _oldScheme; }
        }
    }
}