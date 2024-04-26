using Internal.Scopes.Abstract.Lifetimes;

namespace Common.Components.Abstract
{
    public interface IGameObjectLifetime
    {
        IReadOnlyLifetime GetValidLifetime();
    }
}