using Cysharp.Threading.Tasks;
using Global.Setup.Scope;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Setup.Runtime;
using UnityEngine;
using VContainer;

namespace Global.Setup.Initial
{
    [DisallowMultipleComponent]
    public class GameSetup : MonoBehaviour
    {
        [SerializeField] private InternalScopeConfig _internal;
        [SerializeField] private GlobalScopeConfig _global;
        [SerializeField] private SetupLoadingScreen _loading;

        private void Awake()
        {
            Setup().Forget();
        }

        private async UniTask Setup()
        {
            var internalScopeLoader = new InternalScopeLoader(_internal);
            var internalScope = await internalScopeLoader.Load();
            var scopeLoaderFactory = internalScope.Container.Resolve<IServiceScopeLoader>();
            var scopeLoadResult = await scopeLoaderFactory.Load(internalScope, _global);
            await scopeLoadResult.Callbacks.RunConstruct();

            _loading.Dispose();
        }
    }
}