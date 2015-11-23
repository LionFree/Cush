using System;
using System.Collections.Generic;
using System.Diagnostics;
using Cush.WPF.ColorSchemes.Helpers;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes
{
    [DebuggerDisplay("Theme={_currentScheme.Theme.DisplayName}, Accent={_currentScheme.Accent.DisplayName}")]
    public class ColorSchemeManager : ResourceContainer
    {
        public delegate void ColorSchemeChangedEventHandler(object sender, ColorSchemeChangedEventArgs e);

        private readonly IKeyedResourceContainer _baseTheme;
        private readonly IColorSchemeExtensionContainer _container;
        private readonly ResourceHelper _helper;
        private IColorScheme _currentScheme;
        private IColorScheme _oldScheme;

        public ColorSchemeManager(IColorSchemeExtensionContainer container) : this(container, new ResourceHelper())
        {
        }

        internal ColorSchemeManager(IColorSchemeExtensionContainer container, ResourceHelper helper)
        {
            _helper = helper;
            _container = container;
            CurrentScheme = container.DefaultScheme;
            _baseTheme = container.BaseTheme;
        }

        public IKeyedResourceContainer BaseTheme
        {
            get { return _baseTheme; }
        }

        public List<IKeyedResourceContainer> Accents
        {
            get { return _container.Accents; }
        }

        public List<IKeyedResourceContainer> Themes
        {
            get { return _container.Themes; }
        }

        public IColorScheme CurrentScheme
        {
            get { return _currentScheme; }
            protected set
            {
                if (_currentScheme == value) return;
                _oldScheme = _currentScheme;
                _currentScheme = value;
                OnSchemeChanged();
            }
        }

        private void ReplaceAccent(IKeyedResourceContainer newValue, ISchemedElement element)
        {
            if (element.CurrentScheme.Accent == newValue) return;

            _helper.RemoveDictionary(element.CurrentScheme.Accent.Resources, element.Resources);
            _helper.AddDictionaryIfMissing(_currentScheme.Accent.Resources, element.Resources);

            element.CurrentScheme.Accent = newValue;
        }

        private void ReplaceTheme(IKeyedResourceContainer newValue, ISchemedElement element)
        {
            if (element.CurrentScheme.Theme == newValue) return;

            _helper.RemoveDictionary(element.CurrentScheme.Theme.Resources, element.Resources);
            _helper.AddDictionaryIfMissing(_currentScheme.Theme.Resources, element.Resources);

            element.CurrentScheme.Theme = newValue;
        }

        public void UpdateColorScheme(ISchemedElement element)
        {
            if (element.CurrentScheme == _currentScheme) return;

            ReplaceAccent(_currentScheme.Accent, element);
            ReplaceTheme(_currentScheme.Theme, element);

            _helper.AddDictionaryIfMissing(_baseTheme.Resources, element.Resources);
        }

        public void SetTheme(IKeyedResourceContainer newValue)
        {
            if (null == newValue) throw new ArgumentNullException("newValue");
            if (_currentScheme.Theme.Guid == newValue.Guid) return;

            CurrentScheme.Theme = newValue;
            OnSchemeChanged();
        }

        public void SetAccent(IKeyedResourceContainer newValue)
        {
            if (null == newValue) throw new ArgumentNullException("newValue");
            if (_currentScheme.Accent.Guid == newValue.Guid) return;

            CurrentScheme.Accent = newValue;
            OnSchemeChanged();
        }

        public event ColorSchemeChangedEventHandler ColorSchemeChanged;
        
        protected virtual void OnSchemeChanged()
        {
            var handler = ColorSchemeChanged;
            if (handler != null)
            {
                handler(this, new ColorSchemeChangedEventArgs(_oldScheme, _currentScheme));
            }
        }
    }
}