using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Lifetimes;
using Internal.Services.Options.Implementations;

namespace Features.Tutorial.Runtime.Steps.SpeedModifications
{
    public class TutorialStep_SpeedModifications : ITutorialStep
    {
        public TutorialStep_SpeedModifications(PlatformOptions platformOptions, TutorialStep_SpeedModifications_UI ui)
        {
            _platformOptions = platformOptions;
            _ui = ui;
        }

        private readonly PlatformOptions _platformOptions;
        private readonly TutorialStep_SpeedModifications_UI _ui;

        public UniTask Handle(IReadOnlyLifetime stepLifetime)
        {
            _ui.SetPlatform(_platformOptions);
            _ui.Enter(stepLifetime);
            var isClicked = false;

            _ui.Clicked.Listen(stepLifetime, () => isClicked = true);
            return UniTask.WaitUntil(() => isClicked == true); 
        }
    }
}