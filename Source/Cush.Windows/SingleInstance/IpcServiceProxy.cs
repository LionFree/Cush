using System;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;

namespace Cush.Windows.SingleInstance
{
    internal abstract class IpcServiceProxy
    {
        /// <summary>
        ///     The name that the system uses to identify the currently-running process to the user.
        /// </summary>
        public abstract string CurrentProcessName { get; }

        internal static IpcServiceProxy GetInstance()
        {
            return new IpcServiceProxyImpl();
        }

        /// <summary>
        ///     Create an IPC client channel to make SingleInstanceProxy object requests
        /// </summary>
        public abstract IChannel NewClientChannel();

        /// <summary>
        ///     Registers an object Type on the service end,
        ///     so it is accessible by IPC client channels.
        /// </summary>
        /// <param name="objectUri">The object URI.</param>
        public abstract void RegisterType<T>(string objectUri);

        /// <summary>
        ///     Create an IPC server channel to listen for SingleInstanceProxy object requests
        /// </summary>
        public abstract IChannel CreateServerChannel(string channelName);

        /// <summary>
        ///     Registers a channel with the channel services, with security disabled.
        /// </summary>
        /// <param name="channel">The channel to register.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="channel" /> parameter is null. </exception>
        /// <exception cref="T:System.Runtime.Remoting.RemotingException">The channel has already been registered. </exception>
        /// <exception cref="T:System.Security.SecurityException">
        ///     At least one of the callers higher in the call stack does not
        ///     have permission to configure remoting types and channels.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        ///     Not supported in Windows 98 for
        ///     <see cref="T:System.Runtime.Remoting.Channels.Tcp.TcpServerChannel" /> and on all platforms for
        ///     <see cref="T:System.Runtime.Remoting.Channels.Http.HttpServerChannel" />. Host the service using Internet
        ///     Information Services (IIS) if you require a secure HTTP channel.
        /// </exception>
        public abstract void RegisterChannel(IChannel channel);

        /// <summary>
        ///     Publish the first proxy object so IPC clients requesting a proxy would receive a reference to it
        /// </summary>
        public abstract void Marshal(MarshalByRefObject obj, string uri);

        /// <summary>
        ///     Unregisters a particular channel from the registered channels list.
        /// </summary>
        /// <param name="channel">The channel to unregister. </param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="channel" /> parameter is null. </exception>
        /// <exception cref="T:System.ArgumentException">The channel is not registered. </exception>
        /// <exception cref="T:System.Security.SecurityException">
        ///     At least one of the callers higher in the callstack does not have
        ///     permission to configure remoting types and channels.
        /// </exception>
        public abstract void UnregisterChannel(IChannel channel);

        /// <summary>
        ///     Creates a proxy for a well-known object, given the Type and URL.
        /// </summary>
        /// <typeparam name="T">The Type of a well-known object on the server end to which you want to connect.</typeparam>
        /// <param name="url">The URL of the server class.</param>
        public abstract T Connect<T>(string url);

        private class IpcServiceProxyImpl : IpcServiceProxy
        {
            public override string CurrentProcessName
            {
                get { return Process.GetCurrentProcess().ProcessName; }
            }

            public override IChannel NewClientChannel()
            {
                return new IpcClientChannel();
                //return _factory.GetClientChannel();
            }

            public override IChannel CreateServerChannel(string channelName)
            {
                //return _factory.GetServerChannel(channelName);
                return new IpcServerChannel(channelName);
                //return ChannelWrapper.GetInstance(new IpcServerChannel(channelName));
            }

            public override void RegisterChannel(IChannel channel)
            {
                ChannelServices.RegisterChannel(channel, true);
            }

            public override void RegisterType<T>(string objectUri)
            {
                RemotingConfiguration.RegisterWellKnownServiceType(
                    typeof (T),
                    objectUri,
                    WellKnownObjectMode.Singleton);
            }

            public override void Marshal(MarshalByRefObject obj, string uri)
            {
                RemotingServices.Marshal(obj, uri);
            }

            public override void UnregisterChannel(IChannel channel)
            {
                ChannelServices.UnregisterChannel(channel);
            }

            public override T Connect<T>(string uri)
            {
                return (T) RemotingServices.Connect(typeof (T), uri);
            }
        }
    }
}