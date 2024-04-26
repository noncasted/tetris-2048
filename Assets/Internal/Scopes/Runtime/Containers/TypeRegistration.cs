using System;
using Internal.Scopes.Abstract.Callbacks;
using Internal.Scopes.Abstract.Containers;
using VContainer;

namespace Internal.Scopes.Runtime.Containers
{
    public class TypeRegistration : IRegistration
    {
            public TypeRegistration(RegistrationBuilder builder, Type type)
        {
            Builder = builder;
            Type = type;
        }

        private bool _isListeningCallbacks;
        private bool _isSelfResolvable;
        
        public readonly RegistrationBuilder Builder;

        public Type Type { get; }
        public bool IsCallbackListener => _isListeningCallbacks;
        public bool IsSelfResolvable => _isSelfResolvable;

        public IRegistration AsCallbackListener()
        {
            _isListeningCallbacks = true;
            _isSelfResolvable = true;

            return AsSelfResolvable();
        }

        public IRegistration AsSelf()
        {
            Builder.AsSelf();

            return this;
        }

        public IRegistration AsImplementedInterfaces()
        {
            Builder.AsImplementedInterfaces();

            return this;
        }

        public IRegistration AsSelfResolvable()
        {
            _isSelfResolvable = true;
            Builder.AsSelf();

            return this;
        }

        public IRegistration As<T>()
        {
            Builder.As<T>();

            return this;
        }

        public IRegistration WithParameter<T>(T value)
        {
            Builder.WithParameter(value);

            return this;
        }

        public IRegistration WithParameter<T>(T value, string name)
        {
            Builder.WithParameter(name, value);

            return this;
        }

        public void Register(IContainerBuilder builder)
        {
            builder.Register(Builder);
        }

        public void Resolve(IObjectResolver resolver)
        {
            if (_isSelfResolvable == false)
                return;

            resolver.Resolve(Type);
        }

        public void ResolveWithCallbacks(IObjectResolver resolver, ICallbacksRegistry callbackRegistry)
        {
            if (_isSelfResolvable == false)
                return;

            var registration = resolver.Resolve(Type);

            if (_isListeningCallbacks == true)
                callbackRegistry.Listen(registration);
        }
    }
}