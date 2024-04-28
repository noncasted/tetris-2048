using Common.DataTypes.Runtime.Structs;
using Cysharp.Threading.Tasks;
using GamePlay.Save.Abstract;
using Internal.Scopes.Abstract.Lifetimes;

namespace GamePlay.Boards.Abstract.Boards
{
    public interface IBoard
    {
        void Fill(IReadOnlyLifetime lifetime, GameSave save);
        UniTask<BoardHandle> CreateHandle(IReadOnlyLifetime lifetime);
        void OnInput(CoordinateDirection direction);
    }
}