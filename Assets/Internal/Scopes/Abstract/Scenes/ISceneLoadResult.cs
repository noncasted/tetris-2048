using UnityEngine.SceneManagement;

namespace Internal.Scopes.Abstract.Scenes
{
    public interface ISceneLoadResult
    {
        Scene Scene { get; }
    }
}