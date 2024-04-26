namespace Global.Configs.Upgrades.Abstract
{
    public interface IUpgradeInstance
    {
        IUpgradeDefinition Definition { get; }
        int CurrentLevel { get; }
        bool IsMaxLevel { get; }

        void SetLevel(int level);
    }
}