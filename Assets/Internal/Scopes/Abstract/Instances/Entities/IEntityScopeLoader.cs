using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace Internal.Scopes.Abstract.Instances.Entities
{
    public interface IEntityScopeLoader
    {
        UniTask<T> Load<T>(
            LifetimeScope parent,
            IScopedEntityViewFactory viewFactory,
            IScopedEntityConfig config);
        
        UniTask<T> Load<T>(
            LifetimeScope parent,
            IScopedEntityViewFactory viewFactory,
            IScopedEntityConfig config,
            IReadOnlyList<IComponentFactory> runtimeFactories);
    }
}