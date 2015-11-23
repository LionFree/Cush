namespace Cush.MVVM
{
    public interface IFactory<TFactory>
    {
        TProduct Build<TProduct>(params object[] args) where TProduct : IProductOf<TFactory>;
    }

    public abstract class Factory<TFactory> where TFactory : IFactory<TFactory>, new()
    {
        public static Factory<TFactory> GetInstance()
        {
            return new FactoryImplementation();
        }

        public abstract TProduct Create<TProduct>(params object[] args) where TProduct : IProductOf<TFactory>;

        private class FactoryImplementation : Factory<TFactory>
        {
            public override TProduct Create<TProduct>(params object[] args)
            {
                return new TFactory().Build<TProduct>(args);
            }
        }
    }
}