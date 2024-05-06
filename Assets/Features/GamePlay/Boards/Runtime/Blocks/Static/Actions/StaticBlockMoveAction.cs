using Global.System.Updaters.Progressions;
using Internal.Scopes.Abstract.Lifetimes;

namespace Features.GamePlay.Boards.Runtime.Blocks.Static.Actions
{
    public class StaticBlockMoveAction : IStaticBlockAction
    {
        public StaticBlockMoveAction(
            IReadOnlyLifetime lifetime,
            IProgressionHandle progressionHandle,
            IStaticBlockInteractor interactor,
            StaticBlockMoveOptions options)
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
        private readonly IStaticBlockInteractor _interactor;
        private readonly StaticBlockMoveOptions _options;

        public void Start()
        {
            _options.Start.RemoveBlock(_options.Self); 
            _options.Target.SetBlock(_options.Self);
            _interactor.SetTile(_options.Target);
            
            _progressionHandle.Start(_lifetime, _options.Config.MoveTime, HandleProgress);
        }
        
        public void HandleProgress(float progress)
        {
            _mover.Move(progress);
            
            if (progress < 1f)
                return;
            
            _lifetime.Terminate();
        }

        public void Break()
        {
            _lifetime.Terminate();
            _mover.Complete();
        }
    }
}