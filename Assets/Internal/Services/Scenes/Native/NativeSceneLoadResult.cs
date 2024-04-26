using Internal.Scopes.Abstract.Scenes;
using UnityEngine.SceneManagement;

namespace Internal.Services.Scenes.Native
{
    public class NativeSceneLoadResult : ISceneLoadResult
    {
        public NativeSceneLoadResult(Scene scene)
        {
            _scene = scene;
        }
        
        private readonly Scene _scene;

        public Scene Scene => _scene;
    }
}