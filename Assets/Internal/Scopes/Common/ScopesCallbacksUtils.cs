using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Common.Services;

namespace Internal.Scopes.Common
{
    public static class ScopesCallbacksUtils
    {
        public static void AddServiceCallbacks(this IServiceScopeUtils utils)
        {
            var callbacks = new ServiceDefaultCallbacksRegister();
            callbacks.AddCallbacks(utils.Callbacks, utils.Data);
        }
    }
}