using Common.DataTypes.Runtime.Reactive;

namespace GamePlay.Boards.Abstract.Blocks
{
    public interface IBoardBlockValue
    {
        bool IsUpgradeStarted { get; }
        int Number { get; }
        IViewableDelegate<int, BlockUpgradeData> UpgradeStarted { get; }
        
        bool CanBeMergedWith(int value);
        void StartUpgrade(BlockUpgradeData data);
        void CompleteUpgrade();
    }
}