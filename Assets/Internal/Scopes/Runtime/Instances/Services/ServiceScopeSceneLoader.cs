using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Scenes;

namespace Internal.Scopes.Runtime.Instances.Services
{
    public class ServiceScopeSceneLoader : ISceneLoader
    {
        public ServiceScopeSceneLoader(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private readonly List<ISceneLoadResult> _results = new();
        private readonly ISceneLoader _sceneLoader;

        public IReadOnlyList<ISceneLoadResult> Results => _results;

        public async UniTask<ISceneLoadResult> Load(SceneData sceneAsset)
        {
            var result = await _sceneLoader.Load(sceneAsset);

            _results.Add(result);

            return result;
        }
    }
}