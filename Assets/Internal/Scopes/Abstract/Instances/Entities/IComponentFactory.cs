using Internal.Scopes.Abstract.Containers;

namespace Internal.Scopes.Abstract.Instances.Entities
{
    public interface IComponentFactory
    {
        void Create(IServiceCollection services, IScopedEntityUtils utils);
    }
}