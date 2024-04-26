using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using UnityEngine;

namespace Global.Publisher.Abstract.Bootstrap
{
    public abstract class PublisherSdkFactory : ScriptableObject, IServiceFactory
    {
        public abstract UniTask Create(IServiceCollection builder, IServiceScopeUtils utils);
    }
}