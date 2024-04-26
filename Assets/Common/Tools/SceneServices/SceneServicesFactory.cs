using Internal.Scopes.Abstract.Containers;
using UnityEngine;

namespace Common.Tools.SceneServices
{
    public abstract class SceneServicesFactory : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour[] _autoRegister;
        [SerializeField] private MonoBehaviour[] _injections;

        public void Create(IServiceCollection services)
        {
            foreach (var service in _autoRegister)
            {
                services.RegisterComponent(service, service.GetType())
                    .AsImplementedInterfaces()
                    .AsCallbackListener();
            }

            foreach (var injection in _injections)
                services.Inject(injection);
            
            CollectServices(services);
        }

        protected abstract void CollectServices(IServiceCollection services);
    }
}