using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Scenes;
using UnityEngine.SceneManagement;

namespace Internal.Services.Scenes.Native
{
    public class NativeSceneLoader : ISceneLoader
    {
        public async UniTask<ISceneLoadResult> Load(SceneData data)
        {
            var targetScene = new Scene();

            SceneManager.sceneLoaded += OnSceneLoaded;

            void OnSceneLoaded(Scene loadedScene, LoadSceneMode mode)
            {
                if (loadedScene.name != data.Scene.SceneName)
                    return;

                targetScene = loadedScene;
                SceneManager.sceneLoaded -= OnSceneLoaded;
            }

            var handle = SceneManager.LoadSceneAsync(data.Scene.SceneName, LoadSceneMode.Additive);
            var task = handle.ToUniTask();
            await task;

            await UniTask.WaitUntil(() => targetScene.name == data.Scene.SceneName);

            var result = new NativeSceneLoadResult(targetScene);
            
            return result;
        }
    }
}