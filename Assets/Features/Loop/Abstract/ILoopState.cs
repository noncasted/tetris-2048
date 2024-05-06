using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Lifetimes;

namespace Features.Loop.Abstract
{
    public interface ILoopState
    {
        UniTask<GameStateType> Enter(IReadOnlyLifetime stateLifetime);
    }
}