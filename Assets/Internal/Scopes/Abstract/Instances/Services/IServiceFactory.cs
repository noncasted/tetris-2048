using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Containers;

namespace Internal.Scopes.Abstract.Instances.Services
{
    public interface IServiceFactory
    {
        UniTask Create(IServiceCollection services, IServiceScopeUtils utils);
    }
}