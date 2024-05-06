using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Lifetimes;

namespace Features.Tutorial.Runtime.Steps
{
    public interface ITutorialStep
    {
        UniTask Handle(IReadOnlyLifetime stepLifetime);
    }
}