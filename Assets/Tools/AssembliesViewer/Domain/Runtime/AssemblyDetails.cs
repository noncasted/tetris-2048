using System.Collections.Generic;
using Tools.AssembliesViewer.Domain.Abstract;

namespace Tools.AssembliesViewer.Domain.Runtime
{
    public class AssemblyDetails : IAssemblyDetails
    {
        public AssemblyDetails(
            IReadOnlyList<string> namespaces,
            IReadOnlyList<string> usings,
            IReadOnlyList<string> interfaces,
            bool isOwned)
        {
            Namespaces = namespaces;
            Usings = usings;
            Interfaces = interfaces;
            IsOwned = isOwned;
        }

        public IReadOnlyList<string> Namespaces { get; }
        public IReadOnlyList<string> Usings { get; }
        public IReadOnlyList<string> Interfaces { get; }
        public bool IsOwned { get; }
    }
}