using System;
using System.Globalization;
using System.Reflection;

namespace Cush.MVVM
{
    public class FactoryProduct<TFactory> : IFactory<TFactory> where TFactory : IFactory<TFactory>
    {
        [STAThread]
        public TProduct Build<TProduct>(params object[] args) where TProduct : IProductOf<TFactory>
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            return (TProduct) Activator.CreateInstance(typeof (TProduct), flags,
                null, args, CultureInfo.InvariantCulture);
        }
    }
}