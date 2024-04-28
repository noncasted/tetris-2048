using Cysharp.Threading.Tasks;
using Global.UI.StateMachines.Abstract;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.UI.StateMachines.Runtime
{
    [InlineEditor]
    public class UiStateMachineFactory : ScriptableObject, IServiceFactory
    {
        public UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            services.Register<UiStateMachine>()
                .As<IUiStateMachine>();
            
            return UniTask.CompletedTask;
        }
    }
}