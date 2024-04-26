using Cysharp.Threading.Tasks;
using Global.Inputs.Constraints.Abstract;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Inputs.Constraints.Runtime
{
    [InlineEditor]
    public class InputConstraintsFactory : ScriptableObject, IServiceFactory
    {
        public async UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            services.Register<InputConstraintsStorage>()
                .As<IInputConstraintsStorage>()
                .AsCallbackListener();
        }
    }
}