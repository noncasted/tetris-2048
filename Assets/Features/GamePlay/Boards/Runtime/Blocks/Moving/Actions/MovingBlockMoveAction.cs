using Global.System.Updaters.Progressions;
using Internal.Scopes.Abstract.Lifetimes;

namespace Features.GamePlay.Boards.Runtime.Blocks.Moving.Actions
{
    public class MovingBlockMoveAction : IMovingBlockAction
    {
        public MovingBlockMoveAction(
            IReadOnlyLifetime lifetime,
            IProgressionHandle progressionHandle,
            IMovingBlockInteractor interactor,
            MovingBlockMoveOptions options)
        {
            _lifetime = lifetime.CreateChild();
            _progressionHandle = progressionHandle;
            _interactor = interactor;
            _options = options;
            _mover = new BlockMover(options.Start, options.Target, options.Transform, options.Config);
        }

        private readonly BlockMover _mover;
        private readonly ILifetime _lifetime;
        private readonly IProgressionHandle _progressionHandle;
        private readonly IMovingBlockInteractor _interactor;
        private readonly MovingBlockMoveOptions _options;

        public void Start()
        {
            _options.Start.Free(); 
            _options.Target.Take();
            _interactor.SetTile(_options.Target);
            
            _progressionHandle.Start(_lifetime, _options.Config.MoveTime, HandleProgress);
        }
        
        public void HandleProgress(float progress)
        {
            _mover.Move(progress);
            
            if (progress < 1f)
                return;
            
            _lifetime.Terminate();
            _interactor.OnCurrentActionCompleted();
        }

        public void Break()
        {
            _lifetime.Terminate();
            _mover.Complete();
        }
    }
}