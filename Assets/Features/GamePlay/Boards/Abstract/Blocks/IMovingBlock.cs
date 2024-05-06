using Internal.Scopes.Abstract.Lifetimes;

namespace Features.GamePlay.Boards.Abstract.Blocks
{
    public interface IMovingBlock
    {
        IReadOnlyLifetime Lifetime { get; }
    }
}