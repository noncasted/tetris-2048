using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Lifetimes;

namespace Tutorial.Runtime.Steps.SpeedModifications
{
    public class TutorialStep_SpeedModifications : ITutorialStep
    {
        public TutorialStep_SpeedModifications(TutorialStep_SpeedModifications_UI ui)
        {
            _ui = ui;
        }
        
        private readonly TutorialStep_SpeedModifications_UI _ui;

        public UniTask Handle(IReadOnlyLifetime stepLifetime)
        {
            _ui.Enter(stepLifetime);
            var isClicked = false;

            _ui.Clicked.Listen(stepLifetime, () => isClicked = true);
            return UniTask.WaitUntil(() => isClicked == true); 
        }
    }
}