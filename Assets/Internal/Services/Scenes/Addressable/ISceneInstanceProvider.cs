using UnityEngine.ResourceManagement.ResourceProviders;

namespace Internal.Services.Scenes.Addressable
{
    public interface ISceneInstanceProvider
    {
        SceneInstance SceneInstance { get; }
    }
}