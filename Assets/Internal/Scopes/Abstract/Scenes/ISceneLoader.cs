using Cysharp.Threading.Tasks;

namespace Internal.Scopes.Abstract.Scenes
{
    public interface ISceneLoader
    {
        UniTask<ISceneLoadResult> Load(SceneData data);
    }
}