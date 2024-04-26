using Internal.Scopes.Abstract.Containers;
using VContainer.Unity;

namespace Internal.Scopes.Abstract.Instances.Entities
{
    public interface IScopedEntityViewFactory
    {
        public LifetimeScope Scope { get; }
        
        void CreateViews(IServiceCollection services, IScopedEntityUtils utils);
    }
}