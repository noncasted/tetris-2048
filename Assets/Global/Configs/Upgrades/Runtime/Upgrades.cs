using System.Collections.Generic;
using Global.Configs.Upgrades.Abstract;

namespace Global.Configs.Upgrades.Runtime
{
    public class Upgrades : IUpgrades
    {
        private readonly Dictionary<IUpgradeDefinition, IUpgradeInstance> _instances = new();
        
        public void Register(IUpgradeInstance instance)
        {
            _instances.Add(instance.Definition, instance);
        }

        public void Upgrade(IUpgradeDefinition definition)
        {
            var instance = _instances[definition];
            var level = instance.CurrentLevel + 1;
            instance.SetLevel(level);
        }
    }
}