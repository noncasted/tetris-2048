using Internal.Scopes.Abstract.Lifetimes;

namespace GamePlay.Boards.Abstract.Blocks
{
    public interface IMovingBlock
    {
        IReadOnlyLifetime Lifetime { get; }
    }
}