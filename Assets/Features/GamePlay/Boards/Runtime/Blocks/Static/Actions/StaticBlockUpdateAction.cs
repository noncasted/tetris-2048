using Common.DataTypes.Runtime.Structs;
using Features.GamePlay.Boards.Abstract.Blocks;
using Global.System.Updaters.Progressions;
using Internal.Scopes.Abstract.Lifetimes;

namespace Features.GamePlay.Boards.Runtime.Blocks.Static.Actions
{
    public class StaticBlockUpdateAction : IStaticBlockAction
    {
        public StaticBlockUpdateAction(
            IReadOnlyLifetime lifetime,
            IProgressionHandle progressionHandle,
            IStaticBlockInteractor interactor,
            StaticBlockMoveOptions options)
        {
            _lifetime = lifetime.CreateChild();
            _progressionHandle = progressionHandle;
            _interactor = interactor;
            _options = options;
            _target = options.Target.Block;
            _mover = new BlockMover(options.Start, options.Target, options.Transform, options.Config);
        }

        private readonly BlockMover _mover;

        private readonly ILifetime _lifetime;
        private readonly IProgressionHandle _progressionHandle;
        private readonly IStaticBlockInteractor _interactor;
        private readonly StaticBlockMoveOptions _options;
        private readonly IBoardBlock _target;

        public void Start()
        {
            _interactor.SetTile(null);
            _options.Start.RemoveBlock(_options.Self);
            var direction = (_options.Target.BoardPosition - _options.Start.BoardPosition).Normalized();
            _target.Value.StartUpgrade(new BlockUpgradeData(direction));
            _progressionHandle.Start(_lifetime, _options.Config.MoveTime, HandleProgress);
        }
        
        public void HandleProgress(float progress)
        {
            _mover.Move(progress);
            
            if (progress < 1f)
                return;
            
            _target.Value.CompleteUpgrade();
            _interactor.DestroyBlock();
            _lifetime.Terminate();
        }

        public void Break()
        {
            _target.Value.CompleteUpgrade();
            _mover.Complete();
            _lifetime.Terminate();
            _interactor.DestroyBlock();
        }
    }
}