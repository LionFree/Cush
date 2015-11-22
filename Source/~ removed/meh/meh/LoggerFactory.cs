namespace Cush.Logging.meh
{
    public sealed class Logger : IGAFactory<Logger>
    {
        public TProduct Create<TProduct>(params object[] args) where TProduct : IProduct<Logger>, new()
        {
            return new TProduct();
        }

        //public TProduct Create<TProduct>(params object[] args) where TProduct : new()
        //{
        //    return new TProduct();
        //}
    }

    
}