using System.ComponentModel.Composition.Hosting;

namespace Cush.Composition.Interfaces
{
    public interface IImportContainer
    {
        bool ComposeImports(CompositionContainer container);
    }
}