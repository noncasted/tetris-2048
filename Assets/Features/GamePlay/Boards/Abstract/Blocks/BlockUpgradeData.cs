using UnityEngine;

namespace Features.GamePlay.Boards.Abstract.Blocks
{
    public class BlockUpgradeData
    {
        public BlockUpgradeData(Vector2Int direction)
        {
            Direction = direction;
        }

        public readonly Vector2 Direction;
    }
}