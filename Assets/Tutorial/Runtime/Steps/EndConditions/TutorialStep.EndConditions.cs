using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Lifetimes;
using Internal.Services.Options.Implementations;

namespace Tutorial.Runtime.Steps.EndConditions
{
    public class TutorialStep_EndConditions : ITutorialStep
    {
        public TutorialStep_EndConditions(PlatformOptions platformOptions, TutorialStep_EndConditions_UI ui)
        {
            _platformOptions = platformOptions;
            _ui = ui;
        }

        private readonly PlatformOptions _platformOptions;
        private readonly TutorialStep_EndConditions_UI _ui;

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