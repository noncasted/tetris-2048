using System;
using System.Collections.Generic;
using Internal.Scopes.Abstract.Callbacks;
using Internal.Scopes.Abstract.Containers;
using VContainer;
using VContainer.Internal;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Internal.Scopes.Runtime.Containers
{
    public class ServiceCollection : IServiceCollection
    {
        private readonly List<InstanceInjection> _injections = new();
        private readonly List<TypeRegistration> _registrations = new();

        public void PassRegistrations(IContainerBuilder builder)
        {
            foreach (var registration in _registrations)
                builder.Register(registration.Builder);
        }

        public void Resolve(IObjectResolver resolver, ICallbacksListener callbackRegistry)
        {
            foreach (var registration in _registrations)
            {
                if (registration.IsSelfResolvable == false)
                    continue;

                var instance = resolver.Resolve(registration.Type);

                if (registration.IsCallbackListener == true)
                    callbackRegistry.Listen(instance);
            }

            foreach (var injection in _injections)
            {
                injection.Inject(resolver);
                callbackRegistry.Listen(injection.Target);
            }
        }

        public IRegistration Register<T>()
        {
            var type = typeof(T);
            var builder = new RegistrationBuilder(type, Lifetime.Singleton);
            var registration = new TypeRegistration(builder, type);

            _registrations.Add(registration);

            return registration;
        }

        public IRegistration RegisterInstance<T>(T instance)
        {
            var builder = new InstanceRegistrationBuilder(instance).As(typeof(T));
            var registration = new TypeRegistration(builder, typeof(T));
            _registrations.Add(registration);

            return registration;
        }

        public IRegistration RegisterComponent<T>(T component) where T : Object
        {
            var builder = new ComponentRegistrationBuilder(component);
            var registration = new TypeRegistration(builder, typeof(T));

            _registrations.Add(registration);

            return registration;
        }

        public IRegistration RegisterComponent<T>(T component, Type type) where T : Object
        {
            var builder = new ComponentRegistrationBuilder(component);
            var registration = new TypeRegistration(builder, type);

            _registrations.Add(registration);

            return registration;
        }

        public void Inject<T>(T component)
        {
            if (component == null)
                throw new NullReferenceException("No component provided");

            var injection = new InstanceInjection(component);

            _injections.Add(injection);
        }
    }
}