using Features.GamePlay.Boards.Abstract.Blocks;
using Features.GamePlay.Boards.Abstract.Boards;
using Internal.Scopes.Abstract.Lifetimes;
using UnityEngine;

namespace Features.GamePlay.Boards.Abstract.Factory
{
    public interface IBlockFactory
    {
        void AddGamePlayLifetime(IReadOnlyLifetime lifetime);
        void CreateStatic(Vector2Int position, int value);
        IMovingBlock CreateMoving(IReadOnlyLifetime lifetime, IBoardTile tile, int value);
    }
}