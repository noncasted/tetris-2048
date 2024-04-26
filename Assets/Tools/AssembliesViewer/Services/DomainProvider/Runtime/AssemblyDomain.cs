using System.Collections.Generic;
using Tools.AssembliesViewer.Domain.Abstract;
using Tools.AssembliesViewer.Services.DomainProvider.Abstract;

namespace Tools.AssembliesViewer.Services.DomainProvider.Runtime
{
    public class AssemblyDomain : IAssemblyDomain
    {
        public AssemblyDomain(IReadOnlyList<IAssembly> assemblies)
        {
            Assemblies = assemblies;
        }
 
        public IReadOnlyList<IAssembly> Assemblies { get; }
    }
}