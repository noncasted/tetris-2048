using System;
using Internal.Scopes.Abstract.Lifetimes;

namespace Global.System.MessageBrokers.Abstract
{
    public interface IMessageBroker
    {
        void Publish<T>(T payload);
        void Listen<T>(IReadOnlyLifetime lifetime, Action<T> listener);
    }
}