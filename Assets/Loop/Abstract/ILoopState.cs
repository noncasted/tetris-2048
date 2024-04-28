using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Lifetimes;

namespace Loop.Abstract
{
    public interface ILoopState
    {
        UniTask<GameStateType> Enter(IReadOnlyLifetime stateLifetime);
    }
}