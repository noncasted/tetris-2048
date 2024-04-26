using Common.DataTypes.Runtime.Structs;
using Cysharp.Threading.Tasks;
using GamePlay.Save.Abstract;
using Internal.Scopes.Abstract.Lifetimes;

namespace GamePlay.Loop.Abstract
{
    public interface IGamePlayLoop
    {
        void Construct(IReadOnlyLifetime lifetime, GameSave save);
        UniTask<GameResult> HandleGame(IReadOnlyLifetime lifetime);
        void PassInput(CoordinateDirection direction);
    }
}