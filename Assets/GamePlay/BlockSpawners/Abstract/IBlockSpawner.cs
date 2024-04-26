using Internal.Scopes.Abstract.Lifetimes;

namespace GamePlay.BlockSpawners.Abstract
{
    public interface IBlockSpawner
    {
        void Start(IReadOnlyLifetime lifetime);
    }
}