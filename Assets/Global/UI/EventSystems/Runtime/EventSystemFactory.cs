using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Global.UI.EventSystems.Runtime
{
    [InlineEditor]
    public class EventSystemFactory : ScriptableObject, IServiceFactory
    {
        [SerializeField] private EventSystem _prefab;
        
        public async UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            var eventSystem = Instantiate(_prefab);
            eventSystem.name = "EventSystem";

            utils.Binder.MoveToModules(eventSystem);
        }
    }
}