using System.Runtime.Remoting.Channels.Ipc;

namespace Cush.Common.SingleInstance
{
    public abstract class IpcChannelFactory
    {
        public static IpcChannelFactory GetInstance()
        {
            return new ChannelFactoryImpl();
        }

        public abstract ChannelWrapper GetServerChannel(string channelName);
        public abstract ChannelWrapper GetClientChannel();

        private class ChannelFactoryImpl : IpcChannelFactory
        {
            public override ChannelWrapper GetServerChannel(string channelName)
            {
                return ChannelWrapper.GetInstance(new IpcServerChannel(channelName));
            }

            public override ChannelWrapper GetClientChannel()
            {
                return ChannelWrapper.GetInstance(new IpcClientChannel());
            }
        }
    }
}