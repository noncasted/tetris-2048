using System.Collections.Generic;

namespace Internal.Scopes.Abstract.Instances.Services
{
    public interface IServicesCompose
    {
        IReadOnlyList<IServiceFactory> Factories { get; }
    }
}