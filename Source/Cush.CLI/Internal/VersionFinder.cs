﻿using System;
using Cush.Common;
using Cush.Common.Exceptions;

namespace Cush.CommandLine.Internal
{
    internal abstract class VersionFinder
    {
        internal static VersionFinder GetInstance(string appName, IBuildInfo info)
        {
            return new VersionFinderImpl(appName, info);
        }

        public abstract string GetVersion();

        private class VersionFinderImpl : VersionFinder
        {
            private readonly string _appName;
            private readonly IBuildInfo _info;

            internal VersionFinderImpl(string appName, IBuildInfo info)
            {
                ThrowHelper.IfNullOrEmptyThenThrow(() => appName);
                ThrowHelper.IfNullThenThrow(() => info);
                _appName = appName;
                _info = info;
            }

            public override string GetVersion()
            {
                var ver = _info.GetVersion();
                return string.Format(Environment.NewLine + "{0} [Version {1}]", _appName, ver);
            }
        }
    }
}