using System.Collections.Generic;

namespace Tools.AssembliesViewer.Domain.Abstract
{
    public interface IAssemblyDetails
    {
        IReadOnlyList<string> Namespaces { get; }
        IReadOnlyList<string> Usings { get; }
        IReadOnlyList<string> Interfaces { get; }
        bool IsOwned { get; }
    }
}