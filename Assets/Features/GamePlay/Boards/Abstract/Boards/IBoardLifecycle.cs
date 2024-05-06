using Common.DataTypes.Runtime.Reactive;

namespace Features.GamePlay.Boards.Abstract.Boards
{
    public interface IBoardLifecycle
    {
        IViewableDelegate MoveStarted { get; }
        IViewableDelegate BoardClear { get; }
        
        void OnMoveStarted();
        void OnBoardClear();
    }
}