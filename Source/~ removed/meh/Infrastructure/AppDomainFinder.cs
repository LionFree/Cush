using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Cush.Common.Exceptions;
using mscoree;

namespace meh.Infrastructure
{
    public static class AppDomainFinder
    {
        /// <summary>
        ///     Returns the primary application domain.
        /// </summary>
        /// <returns>The primary application domain.</returns>
        public static AppDomain GetPrimaryAppDomain()
        {
            return GetAppDomain(Process.GetCurrentProcess().MainModule.ModuleName);
        }

        /// <summary>
        ///     Returns the application domain with the given friendly name.
        /// </summary>
        /// <param name="friendlyName">The friendly name of the application domain.</param>
        /// <returns>The application domain with the given friendly name.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if friendlyName is null.</exception>
        public static AppDomain GetAppDomain(string friendlyName)
        {
            ThrowHelper.IfNullThenThrow(() => friendlyName);
            var handle = IntPtr.Zero;

#if NET20
            CorRuntimeHost host = new CorRuntimeHostClass();
#else
            ICorRuntimeHost host = new CorRuntimeHost();
#endif
            try
            {
                host.EnumDomains(out handle);
                while (true)
                {
                    object domain;
                    host.NextDomain(handle, out domain);
                    if (domain == null)
                    {
                        return null;
                    }
                    var appDomain = (AppDomain) domain;
                    if (appDomain.FriendlyName == friendlyName)
                    {
                        return appDomain;
                    }
                }
            }
            finally
            {
                host.CloseEnum(handle);
                Marshal.ReleaseComObject(host);
                
                // ReSharper disable once RedundantAssignment
                host = null;
            }
        }
    }
}