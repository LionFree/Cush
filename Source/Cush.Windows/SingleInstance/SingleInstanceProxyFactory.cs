namespace Cush.Windows.SingleInstance
{
    internal class SingleInstanceProxyFactory
    {
        public SingleInstanceProxy GetProxy(SingleInstanceDelegate @delegate)
        {
            return SingleInstanceProxy.GetInstance(@delegate);
        }
    }
}