using System.ComponentModel.Composition.Hosting;

// ReSharper disable CheckNamespace
namespace Cush.Composition.Interfaces
{
    public interface IImportContainer
    {
        bool ComposeImports(CompositionContainer container);
    }
}