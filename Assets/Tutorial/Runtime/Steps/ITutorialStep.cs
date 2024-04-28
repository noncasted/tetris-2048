using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Lifetimes;

namespace Tutorial.Runtime.Steps
{
    public interface ITutorialStep
    {
        UniTask Handle(IReadOnlyLifetime stepLifetime);
    }
}