using System.Collections.Generic;
using Tools.AssembliesViewer.Domain.Abstract;

namespace Tools.AssembliesViewer.Services.DomainProvider.Abstract
{
    public interface IAssemblyDomain
    {
        IReadOnlyList<IAssembly> Assemblies { get; }
    }
}