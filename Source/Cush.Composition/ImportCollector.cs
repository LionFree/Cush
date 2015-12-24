using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Cush.Common.Logging;
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
        private readonly ILogger _logger;
        private bool _disposed;

        /// <summary>
        ///     Creates a new instance of the ImportCollector class.
        /// </summary>
        public ImportCollector() : this(Loggers.Null)
        {
        }

        /// <summary>
        ///     Creates a new instance of the ImportCollector class
        ///     using the given <see cref="Cush.Common.Logging.ILogger" />
        ///     .
        /// </summary>
        public ImportCollector(ILogger logger) : this(logger, new CompositionContainer())
        {
        }

        /// <summary>
        ///     Creates a new instance of the ImportCollector class
        ///     using the given <see cref="Cush.Common.Logging.ILogger" />
        ///     and <see cref="CompositionContainer" />
        ///     .
        /// </summary>
        private ImportCollector(ILogger logger, CompositionContainer container)
        {
            _logger = logger;
            Container = container;
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

        /// <summary>
        ///     Gets the part definitions that are contained in the catalog.
        /// </summary>
        public IQueryable<ComposablePartDefinition> Parts
        {
            get { return Container.Catalog.Parts; }
        }

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
            Trace.WriteLine(Common.Strings.logDivider);
            _logger.Trace(Strings.StartingImport);
            var catalog = new AggregateCatalog();

            if (SearchByDirectory)
            {
                foreach (var item in DirectoriesToSearch)
                {
                    _logger.Trace(Strings.AddingDirectories, item);
                    catalog.Catalogs.Add(new DirectoryCatalog(item));
                }
            }

            if (SearchByAssembly)
            {
                foreach (var item in AssembliesToSearch)
                {
                    var assy = Assembly.LoadFile(item);
                    _logger.Trace(Strings.AddingAssembly, item);
                    catalog.Catalogs.Add(new AssemblyCatalog(assy));
                }
            }
            Container = new CompositionContainer(catalog);
            Trace.WriteLine(Common.Strings.logDivider);
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