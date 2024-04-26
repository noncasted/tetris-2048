using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Instances.Services;

namespace Global.System.ScopeDisposer.Abstract
{
    public interface IScopeDisposer
    {
        public UniTask Unload(IServiceScopeLoadResult scopeLoadResult);
    }
}