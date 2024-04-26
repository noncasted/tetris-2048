using System;
using Object = UnityEngine.Object;

namespace Internal.Scopes.Abstract.Containers
{
    public interface IServiceCollection
    {
        IRegistration Register<T>();
        IRegistration RegisterInstance<T>(T instance);
        IRegistration RegisterComponent<T>(T component) where T : Object;
        IRegistration RegisterComponent<T>(T component, Type type) where T : Object;
        void Inject<T>(T component);
    }
}