using System.Collections.Generic;
using Global.Configs.Registry.Runtime;
using Global.Configs.Upgrades.Runtime;
using Internal.Scopes.Abstract.Instances.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Configs.Compose
{
    [InlineEditor]
    public class ConfigsCompose : ScriptableObject, IServicesCompose
    {
        [SerializeField] private ConfigsRegistryFactory _registry;
        [SerializeField] private UpgradesFactory _upgrades;

        public IReadOnlyList<IServiceFactory> Factories => new IServiceFactory[]
        {
            _registry,
            _upgrades
        };
    }
}