using Common.DataTypes.Runtime.Structs;
using Cysharp.Threading.Tasks;
using Global.Saves;
using Internal.Scopes.Abstract.Lifetimes;

namespace Features.GamePlay.Boards.Abstract.Boards
{
    public interface IBoard
    {
        void Fill(IReadOnlyLifetime lifetime, GameSave save);
        UniTask<BoardHandle> CreateHandle(IReadOnlyLifetime lifetime);
        void OnInput(CoordinateDirection direction);
    }
}