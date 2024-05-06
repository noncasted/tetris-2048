using Common.DataTypes.Runtime.Structs;
using Cysharp.Threading.Tasks;
using Global.Saves;
using Internal.Scopes.Abstract.Lifetimes;

namespace Features.GamePlay.Loop.Abstract
{
    public interface IGamePlayLoop
    {
        void Construct(IReadOnlyLifetime lifetime, GameSave save);
        UniTask<GameResult> HandleGame(IReadOnlyLifetime lifetime);
        void PassInput(CoordinateDirection direction);
    }
}