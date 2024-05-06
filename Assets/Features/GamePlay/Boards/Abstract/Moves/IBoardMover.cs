using Common.DataTypes.Runtime.Structs;
using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Lifetimes;

namespace Features.GamePlay.Boards.Abstract.Moves
{
    public interface IBoardMover
    {
        UniTask Move(IReadOnlyLifetime lifetime, CoordinateDirection direction);
    }
}