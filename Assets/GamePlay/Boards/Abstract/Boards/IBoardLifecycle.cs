﻿using Common.DataTypes.Runtime.Reactive;

namespace GamePlay.Boards.Abstract.Boards
{
    public interface IBoardLifecycle
    {
        IViewableDelegate MoveStarted { get; }
        IViewableDelegate BoardClear { get; }
        
        void OnMoveStarted();
        void OnBoardClear();
    }
}