using Cysharp.Threading.Tasks;
using Global.Backend.Abstract;
using Global.Backend.Transactions;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Backend.Runtime
{

    [InlineEditor]
    public class BackendFactory : ScriptableObject, IServiceFactory
    {
        public UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            services.Register<BackendClient>()
                .As<IBackendClient>()
                .AsCallbackListener();

            services.Register<TransactionRunner>()
                .As<ITransactionRunner>();
            
            return UniTask.CompletedTask;
        }
    }
}