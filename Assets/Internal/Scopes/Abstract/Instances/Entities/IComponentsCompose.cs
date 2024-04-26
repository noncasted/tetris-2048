using System.Collections.Generic;

namespace Internal.Scopes.Abstract.Instances.Entities
{
    public interface IComponentsCompose
    {
        IReadOnlyList<IComponentFactory> Factories { get; }
    }
}