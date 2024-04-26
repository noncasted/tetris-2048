using GamePlay.Boards.Abstract.Blocks;
using GamePlay.Boards.Abstract.Boards;
using UnityEngine;

namespace GamePlay.Boards.Runtime.Blocks
{
    public class BlockMover
    {
        public BlockMover(
            IBoardTile start,
            IBoardTile target,
            Transform transform,
            BlockConfig config)
        {
            _start = start;
            _target = target;
            _transform = transform;
            _config = config;
        }

        private readonly IBoardTile _start;
        private readonly IBoardTile _target;
        private readonly Transform _transform;
        private readonly BlockConfig _config;

        public void Move(float progress)
        {
            var curveValue = _config.MoveCurve.Evaluate(progress);
            var position = Vector2.Lerp(_start.Position, _target.Position, curveValue);

            _transform.position = position;
        }

        public void Complete()
        {
            _transform.position = _target.Position;
        }
    }
}