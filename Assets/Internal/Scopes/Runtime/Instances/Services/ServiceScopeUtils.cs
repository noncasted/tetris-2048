using Internal.Scopes.Abstract.Callbacks;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Options;
using Internal.Scopes.Abstract.Scenes;
using Internal.Scopes.Runtime.Callbacks;

namespace Internal.Scopes.Runtime.Instances.Services
{
    public class ServiceScopeUtils : IServiceScopeUtils
    {
        public ServiceScopeUtils(
            IOptions options,
            ISceneLoader sceneLoader,
            IServiceScopeBinder binder,
            IServiceScopeData data,
            ScopeCallbacks callbacks,
            bool isMock)
        {
            Options = options;
            SceneLoader = sceneLoader;
            Binder = binder;
            Data = data;
            InternalCallbacks = callbacks;
            IsMock = isMock;
        }

        public IOptions Options { get; }
        public ISceneLoader SceneLoader { get; }
        public IServiceScopeBinder Binder { get; }
        public IServiceScopeData Data { get; }
        public ScopeCallbacks InternalCallbacks { get; }
        public ICallbacksRegistry Callbacks => InternalCallbacks;
        public bool IsMock { get; }
    }
}