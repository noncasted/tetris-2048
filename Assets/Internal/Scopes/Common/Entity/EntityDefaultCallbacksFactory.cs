using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Entities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Internal.Scopes.Common.Entity
{
    [InlineEditor]
    [CreateAssetMenu(fileName = "DefaultEntityCallbacks", menuName = ScopesRoutes.Root + "EntityCallbacks")]
    public class EntityDefaultCallbacksFactory : ScriptableObject, IComponentFactory
    {
        public void Create(IServiceCollection services, IScopedEntityUtils utils)
        {
            var callbacks = new EntityDefaultCallbacksRegister(utils.Callbacks, utils);
            callbacks.AddCallbacks();

            services.RegisterInstance(callbacks)
                .As<IEntityCallbacks>();
        }
    }
}