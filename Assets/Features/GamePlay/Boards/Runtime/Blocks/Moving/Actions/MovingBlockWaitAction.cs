using Features.GamePlay.Boards.Abstract.Blocks;
using Global.System.Updaters.Progressions;
using Internal.Scopes.Abstract.Lifetimes;

namespace Features.GamePlay.Boards.Runtime.Blocks.Moving.Actions
{
    public class MovingBlockWaitAction : IMovingBlockAction
    {
        public MovingBlockWaitAction(
            IReadOnlyLifetime lifetime,
            IProgressionHandle progressionHandle,
            IMovingBlockInteractor interactor,
            BlockConfig config)
        {
            _lifetime = lifetime.CreateChild();
            _progressionHandle = progressionHandle;
            _interactor = interactor;
            _config = config;
        }

        private readonly ILifetime _lifetime;
        private readonly IProgressionHandle _progressionHandle;
        private readonly IMovingBlockInteractor _interactor;
        private readonly BlockConfig _config;
        
        public void Start()
        {
            _progressionHandle.Start(_lifetime, _config.MoveTime, HandleProgress);
        }
        
        public void HandleProgress(float progress)
        {
            if (progress < 1f)
                return;
            
            _lifetime.Terminate();
            _interactor.OnCurrentActionCompleted();
        } 
        public void Break()
        {
            _lifetime.Terminate();
        }
    }
}