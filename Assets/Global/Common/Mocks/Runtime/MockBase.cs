using Cysharp.Threading.Tasks;
using Global.UI.LoadingScreens.Abstract;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Setup.Runtime;
using UnityEngine;
using VContainer;

namespace Global.Common.Mocks.Runtime
{
    [DisallowMultipleComponent]
    public abstract class MockBase : MonoBehaviour
    {
        [SerializeField] private GlobalMockConfig _config;

        private IServiceScopeLoadResult _serviceScopeLoadResult;
        private IServiceScopeLoadResult _childScopeLoadResult;

        public abstract UniTaskVoid Process();

        protected async UniTask<IServiceScopeLoadResult> BootstrapGlobal()
        {
            var internalScopeLoader = new InternalScopeLoader(_config.Internal);
            var internalScope = await internalScopeLoader.Load();
            var scopeLoaderFactory = internalScope.Container.Resolve<IServiceScopeLoader>();
            var scopeLoadResult = await scopeLoaderFactory.Load(internalScope, _config.Global);

            _serviceScopeLoadResult = scopeLoadResult;
            _serviceScopeLoadResult.Scope.Container.Resolve<ILoadingScreen>().HideGameLoading();

            await _serviceScopeLoadResult.Callbacks.RunConstruct();

            return _serviceScopeLoadResult;
        }

        protected async UniTask<IServiceScopeLoadResult> LoadChildScope(IServiceScopeConfig config)
        {
            if (_serviceScopeLoadResult == null)
                await BootstrapGlobal();
            
            var parentScope = _serviceScopeLoadResult.Scope;
            var scopeLoaderFactory = parentScope.Container.Resolve<IServiceScopeLoader>();
            var childLoadResult = await scopeLoaderFactory.Load(parentScope, config);
            _childScopeLoadResult = childLoadResult;

            await childLoadResult.Callbacks.RunConstruct();
            return childLoadResult;
        }

        private void OnApplicationQuit()
        {
            _childScopeLoadResult?.Lifetime.Terminate();
            _serviceScopeLoadResult?.Lifetime.Terminate();
        }
    }
}