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
            Debug.Log("Game setup 0");
            Setup().Forget();
        }

        private async UniTask Setup()
        {
            Debug.Log("Game setup 1");
            var internalScopeLoader = new InternalScopeLoader(_internal);
            Debug.Log("Game setup 2");
            var internalScope = await internalScopeLoader.Load();
            Debug.Log("Game setup 3");
            var scopeLoaderFactory = internalScope.Container.Resolve<IServiceScopeLoader>();
            Debug.Log("Game setup 4");
            var scopeLoadResult = await scopeLoaderFactory.Load(internalScope, _global);
            Debug.Log("Game setup 5");
            await scopeLoadResult.Callbacks.RunConstruct();
            Debug.Log("Game setup 6");

            _loading.Dispose();
        }
    }
}