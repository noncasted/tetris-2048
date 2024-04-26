﻿using Cysharp.Threading.Tasks;
using Global.System.ScopeDisposer.Abstract;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Scenes;

namespace Global.System.ScopeDisposer.Runtime
{
    public class ScopeDisposer : IScopeDisposer
    {
        public ScopeDisposer(ISceneUnloader sceneUnload)
        {
            _sceneUnload = sceneUnload;
        }

        private readonly ISceneUnloader _sceneUnload;

        public async UniTask Unload(IServiceScopeLoadResult scopeLoadResult)
        {
            scopeLoadResult.Lifetime.Terminate();
            await scopeLoadResult.Callbacks.RunDispose();
            await _sceneUnload.Unload(scopeLoadResult.Scenes);
        }
    }
}