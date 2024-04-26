using GamePlay.Boards.Abstract.Blocks;
using GamePlay.Boards.Abstract.Boards;
using UnityEngine;

namespace GamePlay.Boards.Runtime.Blocks.Moving.Actions
{
    public class MovingBlockMoveOptions
    {
        public MovingBlockMoveOptions(
            IBoardTile start,
            IBoardTile target,
            MovingBlock self,
            Transform transform,
            BlockConfig config)
        {
            Start = start;
            Target = target;
            Self = self;
            Transform = transform;
            Config = config;
        }

        public readonly IBoardTile Start;
        public readonly IBoardTile Target;
        public readonly MovingBlock Self;
        public readonly Transform Transform;
        public readonly BlockConfig Config;

    }
}