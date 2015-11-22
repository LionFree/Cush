using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

// ReSharper disable StaticMemberInGenericType

namespace Cush.Common.SingleInstance
{
    internal static class SingleInstanceWPF
    {
        private const string Delimiter = ":";
        private const string ChannelNameSuffix = "SingeInstanceIPCChannel";
        private const string RemoteServiceName = "SingleInstanceApplicationService";
        private const string IpcProtocol = "ipc://";
        private static Mutex _singleInstanceMutex;
        private static IpcServerChannel _channel;
        public static IList<string> CommandLineArgs { get; private set; }

        internal static bool InitializeAsFirstInstance(string uniqueName, bool bringToFront, Action fallback)
        {
            CommandLineArgs = GetCommandLineArgs(uniqueName);
            var name = uniqueName + Environment.UserName;
            var channelName = name + Delimiter + ChannelNameSuffix; //":" + "SingeInstanceIPCChannel";
            bool createdNew;
            _singleInstanceMutex = new Mutex(true, name, out createdNew);
            if (createdNew)
                CreateInstanceOfApplication(channelName);
            else
            {
                if (null != fallback) fallback.Invoke();
                if (bringToFront) BringToFront(Process.GetCurrentProcess().ProcessName);
                SendArgumentsToInstance(channelName, CommandLineArgs);
            }
            return createdNew;
        }

        public static void Cleanup()
        {
            if (_singleInstanceMutex != null)
            {
                _singleInstanceMutex.Close();
                _singleInstanceMutex = null;
            }
            if (_channel == null)
                return;
            ChannelServices.UnregisterChannel(_channel);
            _channel = null;
        }

        #region Private

        private static void BringToFront(string processName)
        {
            const int swRestore = 9;

            // get the list of all processes by that name
            var processes = Process.GetProcessesByName(processName);
            // if this is the only instance, get out.
            if (processes.Length <= 1) return;

            // There is more than one process...
            // Assume there is our process, which we will terminate, 
            // and the other process, which we want to bring to the 
            // foreground. This assumes there are only two processes 
            // in the processes array, and we need to find out which 
            // one is NOT us.

            // get our process
            var p = Process.GetCurrentProcess();
            var n = 0; // assume the other process is at index 0
            // if this process id is OUR process ID...
            if (processes[0].Id == p.Id)
            {
                // then the other process is at index 1
                n = 1;
            }
            // get the window handle
            var hWnd = processes[n].MainWindowHandle;
            // if iconic, we need to restore the window
            if (NativeMethods.IsIconic(hWnd))
            {
                NativeMethods.ShowWindowAsync(hWnd, swRestore);
            }
            // bring it to the foreground
            NativeMethods.SetForegroundWindow(hWnd);
            // exit our process
        }

        private static IList<string> GetCommandLineArgs(string uniqueApplicationName)
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
            if (strArray == null)
                strArray = new string[0];
            return new List<string>(strArray);
        }

        private static void CreateInstanceOfApplication(string channelName)
        {
            var formatterSinkProvider = new BinaryServerFormatterSinkProvider
            {
                TypeFilterLevel = TypeFilterLevel.Full
            };
            IDictionary properties = new Dictionary<string, string>();
            properties["name"] = channelName;
            properties["portName"] = channelName;
            properties["exclusiveAddressUse"] = "false";
            _channel = new IpcServerChannel(properties, formatterSinkProvider);
            ChannelServices.RegisterChannel(_channel, true);
            RemotingServices.Marshal(new WPFIpcRemoteService(), RemoteServiceName);
        }

        private static void SendArgumentsToInstance(string channelName, IList<string> args)
        {
            ChannelServices.RegisterChannel(new IpcClientChannel(), true);
            var ipcRemoteService =
                (WPFIpcRemoteService)
                    RemotingServices.Connect(typeof(WPFIpcRemoteService),
                        IpcProtocol + channelName + @"/" + RemoteServiceName);
            if (ipcRemoteService == null)
                return;
            ipcRemoteService.InvokeFirstInstance(args);
        }


        private static object ActivateFirstInstanceCallback(object arg)
        {
            ActivateFirstInstance((ISingleInstanceApplication) Application.Current, arg as string[]);
            return null;
        }

        private static void ActivateFirstInstance(ISingleInstanceApplication app, string[] args)
        {
            if (app == null) return;
            app.SignalExternalCommandLineArgs(args);
        }





        private class WPFIpcRemoteService : MarshalByRefObject
        {
            public void InvokeFirstInstance(IList<string> args)
            {
                if (Application.Current == null)
                    return;
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    new DispatcherOperationCallback(ActivateFirstInstanceCallback), args);
            }

            public override object InitializeLifetimeService()
            {
                return null;
            }
        }

        #endregion
    }
}