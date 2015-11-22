namespace Cush.Logging.meh
{
    public interface IGAFactory<TFactory>
    {
        //TProduct Create<TProduct>(params object[] args) where TProduct : new();
        TProduct Create<TProduct>(params object[] args) where TProduct : IProduct<TFactory>, new();
    }


    public sealed class GAFactory<TFactory> where TFactory : IGAFactory<TFactory>, new()
    {
        public TProduct Create<TProduct>(params object[] args) where TProduct : IProduct<TFactory>, new()
        {
            return new TFactory().Create<TProduct>(args);
        }
    }

    public interface IProduct<TFactory>
    {
    }
}