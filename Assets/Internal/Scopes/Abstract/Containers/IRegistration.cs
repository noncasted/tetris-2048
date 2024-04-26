using System;

namespace Internal.Scopes.Abstract.Containers
{
    public interface IRegistration
    {
        Type Type { get; }
        
        IRegistration AsCallbackListener();
        IRegistration AsSelf();
        IRegistration AsImplementedInterfaces();
        IRegistration AsSelfResolvable();
        IRegistration As<T>();
        IRegistration WithParameter<T>(T value);
        IRegistration WithParameter<T>(T value, string name);
    }
}