using System.Runtime.Remoting.Channels;

namespace Cush.Common.SingleInstance
{
    public abstract class ChannelWrapper : IChannel
    {
        public abstract string Parse(string url, out string objectURI);
        public abstract int ChannelPriority { get; }
        public abstract string ChannelName { get; }

        public static ChannelWrapper GetInstance(IChannel wrappedObject)
        {
            return new ChannelImpl(wrappedObject);
        }

        private class ChannelImpl : ChannelWrapper
        {
            private readonly IChannel _wrappedObject;

            public ChannelImpl(IChannel wrappedObject)
            {
                _wrappedObject = wrappedObject;
            }

            public override int ChannelPriority
            {
                get { return _wrappedObject.ChannelPriority; }
            }

            public override string ChannelName
            {
                get { return _wrappedObject.ChannelName; }
            }

            public override string Parse(string url, out string objectURI)
            {
                return _wrappedObject.Parse(url, out objectURI);
            }
        }
    }
}