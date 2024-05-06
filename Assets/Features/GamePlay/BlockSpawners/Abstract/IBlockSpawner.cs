using Internal.Scopes.Abstract.Lifetimes;

namespace Features.GamePlay.BlockSpawners.Abstract
{
    public interface IBlockSpawner
    {
        void Start(IReadOnlyLifetime lifetime);
    }
}