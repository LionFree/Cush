using System;
using System.ComponentModel.Composition.Hosting;

// ReSharper disable CheckNamespace
namespace Cush.Composition.Interfaces
{
    public interface IImportCollector : IDisposable
    {
        CompositionContainer Container { get; }

        bool ImportParts();
    }
}