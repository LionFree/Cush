using System;
using System.ComponentModel.Composition.Hosting;

namespace Cush.Composition.Interfaces
{
    public interface IImportCollector : IDisposable
    {
        CompositionContainer Container { get; }

        bool ImportParts();
    }
}