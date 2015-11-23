using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Reflection;
using Cush.Composition.Interfaces;

namespace Cush.Composition
{
    /// <summary>
    ///     Provides methods for gathering plugins (a la MEF attributed imports) from various assemblies.
    ///     For more information on using MEF imports, see
    ///     https://msdn.microsoft.com/en-us/library/dd460648(v=vs.110).aspx
    /// </summary>
    public sealed class ImportCollector : IImportCollector
    {
        private bool _disposed;

        /// <summary>
        ///     Creates a new instance of the ImportCollector class.
        /// </summary>
        public ImportCollector()
        {
            Container = new CompositionContainer();
            SearchByAssembly = true;
            SearchByDirectory = true;
            AssembliesToSearch = new List<string> {Assembly.GetEntryAssembly().Location};
            DirectoriesToSearch = new List<string> {"."};
        }

        /// <summary>
        ///     Determines whether the importer will search dll's by directory.
        /// </summary>
        public bool SearchByDirectory { get; set; }

        /// <summary>
        ///     Determines whether the importer will search dll's by assembly.
        /// </summary>
        public bool SearchByAssembly { get; set; }

        /// <summary>
        ///     A list of directories in which to search all assemblies for importable parts.
        /// </summary>
        public List<string> DirectoriesToSearch { get; set; }

        /// <summary>
        ///     A list of assemblies to search for importable parts.
        /// </summary>
        public List<string> AssembliesToSearch { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Contains all the imported extensions.
        /// </summary>
        public CompositionContainer Container { get; private set; }

        /// <summary>
        ///     Searches and imports MEF-attributed import "parts" into the <see cref="Container" />.
        ///     Does not compose or organize them.
        /// </summary>
        /// <returns><see langword="true" /> if successful, otherwise <see langword="false" />.</returns>
        public bool ImportParts()
        {
            Trace.WriteLine("Starting composition.");
            var catalog = new AggregateCatalog();

            if (SearchByDirectory)
            {
                foreach (var item in DirectoriesToSearch)
                {
                    Trace.WriteLine("  Adding directory {" + item + "}...");
                    catalog.Catalogs.Add(new DirectoryCatalog(item));
                }
            }

            if (SearchByAssembly)
            {
                foreach (var item in AssembliesToSearch)
                {
                    var assy = Assembly.LoadFile(item);
                    Trace.WriteLine("  Adding assembly {" + item + "}...");
                    catalog.Catalogs.Add(new AssemblyCatalog(assy));
                }
            }

            Trace.WriteLine("  Creating Container...");
            Container = new CompositionContainer(catalog);

            return true;
        }

        ~ImportCollector()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                // Dispose managed resources.
                if (Container != null)
                {
                    Container.Dispose();
                }
            }
            _disposed = true;
        }
    }
}