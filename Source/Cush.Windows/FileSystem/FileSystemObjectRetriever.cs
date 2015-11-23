using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Cush.ResourceSystem;

namespace Cush.Windows.FileSystem
{
    internal sealed class FileSystemObjectRetriever : IObjectRetriever
    {
        private DirectoryInfo _directoryInfo;

        [DebuggerStepThrough]
        internal FileSystemObjectRetriever(string path) : this(new DirectoryInfo(path))
        {
        }

        [DebuggerStepThrough]
        internal FileSystemObjectRetriever(DirectoryInfo directoryInfo)
        {
            _directoryInfo = directoryInfo;
        }

        public TOut[] GetObjects<TOut>(string searchPattern = "*") where TOut : IResourceSystemInfo
        {
            try
            {
                UpdateStartingLocation(searchPattern);

                // change the searchPattern to just a file (not a path).
                var finalPattern = Path.GetFileName(searchPattern);
                
                if (string.IsNullOrEmpty(finalPattern)) 
                    return new TOut[0];

                return GetSearchResults<TOut>(finalPattern);
            }
            catch (Exception ex)
            {
                // ignored: return an empty array.
                Trace.WriteLine(ex.Message);
            }

            return new TOut[0];
        }

        private void UpdateStartingLocation(string searchPattern)
        {
            // If the searchPattern includes a path, 
            // then we need to get a new directoryInfo at that path.
            var searchPath = Path.GetDirectoryName(searchPattern);
            
            if (string.IsNullOrEmpty(searchPath)) 
                return;

            if (searchPath != _directoryInfo.FullName) 
                _directoryInfo = new DirectoryInfo(searchPath);
        }

        private TOut[] GetSearchResults<TOut>(string searchPattern) where TOut : IResourceSystemInfo
        {
            TOut[] output;
            var types = typeof (TOut).GetInterfaces();


            if (types.Contains(typeof (ILocationInfo)))
            {
                var input1 = _directoryInfo.GetDirectories(searchPattern);
                output = CreateArray<DirectoryInfo, TOut>(input1);
            }
            else
            {
                var input2 = _directoryInfo.GetFiles(searchPattern);
                output = CreateArray<FileInfo, TOut>(input2);
            }

            return output;
        }

        private TOut[] CreateArray<TIn, TOut>(IReadOnlyList<TIn> array) where TIn : FileSystemInfo
        {
            var output = new TOut[array.Count];
            for (var i = 0; i < array.Count; i++)
            {
                output[i] = (TOut) Activator.CreateInstance(typeof (TOut), array[i].FullName);
            }
            return output;
        }
    }
}