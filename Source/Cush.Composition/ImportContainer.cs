using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
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
        /// <summary>
        /// Composes the imports from the given CompositionContainer into this <see cref="ImportContainer"/>.
        /// </summary>
        /// <param name="container">The <see cref="CompositionContainer"/> containing the imports to compose.</param>
        /// <returns><see langword="true"/> if successful, otherwise <see langword="false"/>.</returns>
        public virtual bool ComposeImports(CompositionContainer container)
        {
            try
            {
                Trace.WriteLine("  Composing imports...");
                container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                ExceptionHelper.ShowException(compositionException);
                return false;
            }

            Trace.WriteLine("  Composition complete.");
            container.Dispose();
            return true;
        }
    }
}
