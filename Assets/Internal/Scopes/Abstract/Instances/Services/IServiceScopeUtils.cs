using Internal.Scopes.Abstract.Callbacks;
using Internal.Scopes.Abstract.Options;
using Internal.Scopes.Abstract.Scenes;

namespace Internal.Scopes.Abstract.Instances.Services
{
    public interface IServiceScopeUtils
    {
        IOptions Options { get; }
        ISceneLoader SceneLoader { get; }
        IServiceScopeBinder Binder { get; }
        IServiceScopeData Data { get; }
        ICallbacksRegistry Callbacks { get; }
        bool IsMock { get; }
    }
}