using Common.DataTypes.Runtime.Reactive;

namespace Features.GamePlay.Loop.Scores.Abstract
{
    public interface IScore
    {
        IViewableProperty<int> Current { get; }
        IViewableProperty<int> Max { get; }

        void SetCurrent(int current);
    }
}