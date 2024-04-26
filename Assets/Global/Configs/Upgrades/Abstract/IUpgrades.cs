namespace Global.Configs.Upgrades.Abstract
{
    public interface IUpgrades
    {
        void Register(IUpgradeInstance instance);
        void Upgrade(IUpgradeDefinition definition);
    }
}