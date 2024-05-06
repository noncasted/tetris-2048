using Common.DataTypes.Runtime.Structs;
using Features.GamePlay.Boards.Abstract.Blocks;
using Global.System.Updaters.Progressions;
using Internal.Scopes.Abstract.Lifetimes;

namespace Features.GamePlay.Boards.Runtime.Blocks.Moving.Actions
{
    public class MovingBlockUpgradeAction : IMovingBlockAction
    {
        public MovingBlockUpgradeAction(
            IReadOnlyLifetime lifetime,
            IProgressionHandle progressionHandle,
            IMovingBlockInteractor interactor,
            MovingBlockMoveOptions options)
        {
            _lifetime = lifetime.CreateChild();
            _progressionHandle = progressionHandle;
            _interactor = interactor;
            _options = options;
            _target = options.Target.Block;
            _mover = new BlockMover(options.Start, options.Target, options.Transform, options.Config);
        }

        private readonly BlockMover _mover;

        private readonly IBoardBlock _target;
        private readonly ILifetime _lifetime;
        private readonly IProgressionHandle _progressionHandle;
        private readonly IMovingBlockInteractor _interactor;
        private readonly MovingBlockMoveOptions _options;

        public void Start()
        {
            _options.Start.Free(); 
            _interactor.SetTile(null);
            var direction = (_options.Target.BoardPosition - _options.Start.BoardPosition).Normalized();
            _target.Value.StartUpgrade(new BlockUpgradeData(direction));
            
            _progressionHandle.Start(_lifetime, _options.Config.MoveTime, HandleProgress);
        }
        
        public void HandleProgress(float progress)
        {
            _mover.Move(progress);
            
            if (progress < 1f)
                return;
            
            _lifetime.Terminate();
            _target.Value.CompleteUpgrade();
            _interactor.DestroyBlock();
        }

        public void Break()
        {
            _lifetime.Terminate();
            _target.Value.CompleteUpgrade();
            _interactor.DestroyBlock();
        }
    }
}