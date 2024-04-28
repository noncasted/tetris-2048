using System;
using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Instances.Services;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Internal.Scopes.Abstract.Scenes
{
    public static class SceneLoaderExtensions
    {
        public static async UniTask<(ISceneLoadResult, T)> LoadTypedResult<T>(this ISceneLoader loader, SceneData data)
        {
            var result = await loader.Load(data);

            var rootObjects = result.Scene.GetRootGameObjects();

            foreach (var rootObject in rootObjects)
            {
                if (rootObject.TryGetComponent(out T searched) == true)
                    return (result, searched);
            }

            throw new NullReferenceException($"Searched {typeof(T)} is not found");
        }
        public static async UniTask<T> LoadTyped<T>(this ISceneLoader loader, SceneData data)

        {
            var result = await loader.Load(data);

            var rootObjects = result.Scene.GetRootGameObjects();

            foreach (var rootObject in rootObjects)
            {
                if (rootObject.TryGetComponent(out T searched) == true)
                    return searched;
            }

            throw new NullReferenceException($"Searched {typeof(T)} is not found");
        }

        public static async UniTask<T> LoadTypedOrGetIfMock<T>(this IServiceScopeUtils utils, SceneData data)
            where T : Object
        {
            if (utils.IsMock == true)
            {
                if (SceneManager.GetSceneByName(data.Scene.SceneName).IsValid() == true)
                    return Object.FindFirstObjectByType<T>();
            }

            return await utils.SceneLoader.LoadTyped<T>(data);
        }
    }
}