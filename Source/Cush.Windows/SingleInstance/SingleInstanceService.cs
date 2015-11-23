using System;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using Cush.Common;
using Cush.Common.Exceptions;

namespace Cush.Windows.SingleInstance
{
    internal abstract class SingleInstanceService : DisposableBase
    {
        public static SingleInstanceService GetInstance()
        {
            return GetInstance(new SingleInstanceProxyFactory(), IpcServiceProxy.GetInstance());
        }

        internal static SingleInstanceService GetInstance(SingleInstanceProxyFactory factory, IpcServiceProxy proxy)
        {
            return new SingleInstanceServiceImpl(factory, proxy);
        }

        /// <summary>
        ///     Attempt to instantiate a single instance of the given ISingleInstanceApplication.
        ///     If one already exists, pass the command line arguments to the existing one.
        /// </summary>
        /// <param name="app">
        ///     The method which would be used to retrieve an ISingleInstanceApplication object when instantiating
        ///     the new object.
        /// </param>
        /// <remarks>
        ///     Note: When using this method and the SingleInstanceDelegate is null, the method can only be used to determine
        ///     whether the application is already running.
        /// </remarks>
        /// <returns><see langword="true" /> if this is the first instance, otherwise <see langword="false" /></returns>
        public abstract bool InstantiateSingleInstance(SingleInstanceDelegate app);

        /// <summary>
        ///     Close the application instance, and dispose of resources.
        /// </summary>
        public abstract void Cleanup();

        private class SingleInstanceServiceImpl : SingleInstanceService
        {
            private const string ProxyObjectName = "SingeInstanceProxy";
            private static Mutex _singleInstanceMutex;
            private static bool _disposed;
            private static IChannel _ipcChannel;
            private static SingleInstanceProxy _proxy;
            private static string _proxyUri;
            private readonly SingleInstanceProxyFactory _factory;
            private readonly IpcServiceProxy _serviceProxy;

            public SingleInstanceServiceImpl(SingleInstanceProxyFactory factory, IpcServiceProxy serviceProxy)
            {
                _factory = factory;
                _serviceProxy = serviceProxy;
            }

            private static string[] CommandLineArgs { get; set; }

            private static string[] GetCommandLineArgs(string uniqueApplicationName)
            {
                string[] strArray = null;
                if (AppDomain.CurrentDomain.ActivationContext == null)
                {
                    strArray = Environment.GetCommandLineArgs();
                }
                else
                {
                    var path =
                        Path.Combine(
                            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                uniqueApplicationName), "cmdline.txt");
                    if (File.Exists(path))
                    {
                        try
                        {
                            using (TextReader textReader = new StreamReader(path, Encoding.Unicode))
                                strArray = NativeMethods.CommandLineToArgvW(textReader.ReadToEnd());
                            File.Delete(path);
                        }
                        catch (IOException)
                        {
                            // Do nothing
                        }
                    }
                }

                return strArray ?? new string[0];
            }

            //private string GetUniqueName()
            //{
            //    var uniqueName = Assembly.GetEntryAssembly().GetName().Name;
            //    ThrowHelper.IfNullOrEmptyThenThrow(() => uniqueName);

            //    return uniqueName + Environment.UserName;
            //    //return name;

            //}


            private void CreateInstanceOfApplication(string channelName,
                SingleInstanceDelegate @delegate)
            {
                // Create an IPC server channel to listen for SingleInstanceProxy object requests
                _ipcChannel = _serviceProxy.CreateServerChannel(channelName);

                // Register the channel and get it ready for use
                _serviceProxy.RegisterChannel(_ipcChannel);

                // Register the service which gets the SingleInstanceProxy object,
                // so it can be accessible by IPC client channels
                _serviceProxy.RegisterType<SingleInstanceProxy>(ProxyObjectName);

                // Attempt to retrieve the enforcer from the delegated method
                _proxy = _factory.GetProxy(@delegate);

                // Publish the first proxy object so IPC clients
                // requesting a proxy would receive a reference to it
                _serviceProxy.Marshal(_proxy, ProxyObjectName);
            }

            private void SendMessageToFirstInstance(string[] args)
            {
                // Create an IPC client channel to request the existing SingleInstanceProxy object.
                _ipcChannel = _serviceProxy.NewClientChannel();

                // Register the channel and get it ready for use
                _serviceProxy.RegisterChannel(_ipcChannel);

                if (_disposed)
                    throw new ObjectDisposedException("The SingleInstanceTracker object has already been disposed.");

                if (_ipcChannel == null)
                    throw new InvalidOperationException(
                        "The object was constructed with the SingleInstanceTracker(string name) constructor overload, or with the SingleInstanceTracker(string name, SingleInstanceEnforcerRetriever enforcerRetriever) constructor overload, with enforcerRetriever set to null, thus you cannot send messages to the first instance.");

                // Retreive a reference to the proxy object which will be later used to send messages
                _proxy = _serviceProxy.Connect<SingleInstanceProxy>(_proxyUri);
                if (_proxy == null) return;

                try
                {
                    // Notify the first instance of the application that a new instance was created
                    _proxy.InvokeFirstInstance(args);
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        "Failed to send message to the first instance of the application. The first instance might have terminated.",
                        ex);
                }
            }

            
            public override bool InstantiateSingleInstance(SingleInstanceDelegate app)
            {
                // Get name info
                var uniqueName = Assembly.GetEntryAssembly().GetName().Name;
                ThrowHelper.IfNullOrEmptyThenThrow(() => uniqueName);
                var name = uniqueName + Environment.UserName;
                _proxyUri = "ipc://" + name + "/" + ProxyObjectName;
                
                
                bool isFirstInstance;

                try
                {
                    _singleInstanceMutex = new Mutex(true, name, out isFirstInstance);

                    if (isFirstInstance)
                        CreateInstanceOfApplication(name, app);
                    else
                    {
                        // Get Commandline args
                        CommandLineArgs = GetCommandLineArgs(uniqueName);
                        SendMessageToFirstInstance(CommandLineArgs);
                    }
                }
                catch (Exception ex)
                {
                    throw new SingleInstancingException(
                        "Failed to instantiate a new SingleInstanceService object.  See InnerException for more details.",
                        ex);
                }
                return isFirstInstance;
            }

            public override void Cleanup()
            {
                Dispose();
            }

            protected override void DisposeOfManagedResources()
            {
                if (_disposed) return;

                if (_singleInstanceMutex != null)
                {
                    _singleInstanceMutex.Close();
                    _singleInstanceMutex = null;
                }

                if (_ipcChannel != null)
                {
                    _serviceProxy.UnregisterChannel(_ipcChannel);
                    _ipcChannel = null;
                }
                _disposed = true;
            }

            protected override void DisposeOfUnManagedObjects()
            {
                // Nothing to do here.
            }
        }
    }
}