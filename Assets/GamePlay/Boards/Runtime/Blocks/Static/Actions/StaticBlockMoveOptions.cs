using GamePlay.Boards.Abstract.Blocks;
using GamePlay.Boards.Abstract.Boards;
using UnityEngine;

namespace GamePlay.Boards.Runtime.Blocks.Static.Actions
{
    public class StaticBlockMoveOptions
    {
        public StaticBlockMoveOptions(
            IBoardTile start,
            IBoardTile target,
            IBoardBlock self,
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
        public readonly IBoardBlock Self;
        public readonly Transform Transform;
        public readonly BlockConfig Config;
    }
}