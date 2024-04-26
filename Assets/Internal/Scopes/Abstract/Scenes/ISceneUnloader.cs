using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Internal.Scopes.Abstract.Scenes
{
    public interface ISceneUnloader
    {
        UniTask Unload(ISceneLoadResult result);
        UniTask Unload(IReadOnlyList<ISceneLoadResult> results);
    }
}