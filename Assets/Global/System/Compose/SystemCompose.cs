using System.Collections.Generic;
using Global.System.ApplicationProxies.Runtime;
using Global.System.MessageBrokers.Runtime;
using Global.System.ScopeDisposer.Runtime;
using Global.System.Updaters.Setup;
using Internal.Scopes.Abstract.Instances.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.System.Compose
{
    [InlineEditor]
    public class SystemCompose : ScriptableObject, IServicesCompose
    {
        [SerializeField] private ScopeDisposerFactory _scopeDisposer;
        [SerializeField] private ApplicationProxyFactory _applicationProxy;
        [SerializeField] private MessageBrokerFactory _messageBroker;
        [SerializeField] private UpdaterFactory _updater;

        public IReadOnlyList<IServiceFactory> Factories => new IServiceFactory[]
        {
            _scopeDisposer,
            _applicationProxy,
            _messageBroker,
            _updater
        };
    }
}