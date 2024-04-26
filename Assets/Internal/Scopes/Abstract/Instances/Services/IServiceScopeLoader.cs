using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace Internal.Scopes.Abstract.Instances.Services
{
    public interface IServiceScopeLoader
    {
        UniTask<IServiceScopeLoadResult> Load(LifetimeScope parent, IServiceScopeConfig config);
    }
}