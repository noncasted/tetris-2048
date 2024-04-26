using System;
using Internal.Scopes.Abstract.Lifetimes;

namespace Global.System.MessageBrokers.Abstract
{
    public static class Msg
    {
        private static IMessageBroker _messageBroker;

        public static void Inject(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }

        public static void Publish<T>(T message)
        {
            _messageBroker.Publish(message);
        }

        public static void Listen<T>(IReadOnlyLifetime lifetime, Action<T> listener)
        {
            _messageBroker.Listen(lifetime, listener);
        }
    }
}