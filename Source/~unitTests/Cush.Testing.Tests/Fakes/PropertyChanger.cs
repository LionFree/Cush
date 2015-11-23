using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Cush.Testing.Tests.Annotations;

namespace Cush.Testing.Tests.Fakes
{
    [ExcludeFromCodeCoverage]
    internal class PropertyChanger : INotifyPropertyChanged
    {
        private int _property1;
        private int _property2;

        public int Property1
        {
            get { return _property1; }
            set
            {
                _property1 = value;
                OnPropertyChanged();
            }
        }

        public int Property2
        {
            get { return _property2; }
            set
            {
                _property2 = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}