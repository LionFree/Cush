using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;
using Cush.Common;
using Cush.Common.Logging;
using Cush.Composition.Helpers;
using Cush.Composition.Interfaces;

namespace Cush.Composition
{
    /// <summary>
    ///     Provides methods for composing imported plugins ("parts") into your application.
    ///     For more information on using MEF imports, see
    ///     https://msdn.microsoft.com/en-us/library/dd460648(v=vs.110).aspx
    /// </summary>
    public abstract class ImportContainer : IImportContainer
    {
        private readonly ExceptionHelper _exceptions;
        protected readonly ILogger Logger;

        protected ImportContainer() : this(Loggers.Null)
        {
        }

        protected ImportContainer(ILogger logger) : this(logger, ExceptionHelper.GetInstance(logger))
        {
        }

        protected ImportContainer(ILogger logger, ExceptionHelper helper)
        {
            Logger = logger;
            _exceptions = helper;
        }

        /// <summary>
        ///     Composes the imports from the given CompositionContainer into this <see cref="ImportContainer" />.
        /// </summary>
        /// <param name="container">The <see cref="CompositionContainer" /> containing the imports to compose.</param>
        /// <returns><see langword="true" /> if successful, otherwise <see langword="false" />.</returns>
        public virtual bool ComposeImports(CompositionContainer container)
        {
            try
            {
                ReportPartsToImport();
                container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                _exceptions.ShowException(compositionException);
                return false;
            }
            catch (ReflectionTypeLoadException reflectionTypeLoadException)
            {
                _exceptions.ShowException(reflectionTypeLoadException);
                return false;
            }
            return true;
        }

        private void ReportPartsToImport()
        {
            Logger.Trace(Strings.ComposingTypes);
            var batch = GetBatch(this);
            var types = RequiredTypeIdentities(batch);
            foreach (var type in types)
            {
                Logger.Trace(Strings.typeBeingComposed, type);
            }
        }

        private static CompositionBatch GetBatch(params object[] attributedParts)
        {
            return new CompositionBatch(attributedParts.Select(
                (AttributedModelServices.CreatePart)).ToArray(),
                Enumerable.Empty<ComposablePart>());
        }

        private static IEnumerable<string> RequiredTypeIdentities(CompositionBatch batch)
        {
            return (from part in batch.PartsToAdd
                from definition in part.ImportDefinitions
                select ((ContractBasedImportDefinition) definition).RequiredTypeIdentity)
                .ToList();
        }


        /// <summary>
        ///     Composes the imports from the given ImportCollector into this <see cref="ImportContainer" />.
        /// </summary>
        /// <param name="collector">The <see cref="ImportCollector" /> containing the imports to compose.</param>
        /// <returns><see langword="true" /> if successful, otherwise <see langword="false" />.</returns>
        public virtual bool ComposeImports(ImportCollector collector)
        {
            return ComposeImports(collector.Container);
        }

        public List<T> GetUniqueExtensions<T>(List<T> list) where T : IKeyedItem, IPartImportsSatisfiedNotification
        {
            var distinctItemGuids = list.Select(x => x.Guid).Distinct().ToList();
            return distinctItemGuids.Count == list.Count
                ? list
                : distinctItemGuids.Select(guid => list.FirstOrDefault(x => x.Guid == guid)).ToList();
        }
    }
}