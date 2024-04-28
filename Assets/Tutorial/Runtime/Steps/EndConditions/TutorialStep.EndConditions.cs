using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Lifetimes;

namespace Tutorial.Runtime.Steps.EndConditions
{
    public class TutorialStep_EndConditions : ITutorialStep
    {
        public TutorialStep_EndConditions(TutorialStep_EndConditions_UI ui)
        {
            _ui = ui;
        }

        private readonly TutorialStep_EndConditions_UI _ui;

        public UniTask Handle(IReadOnlyLifetime stepLifetime)
        {
            _ui.Enter(stepLifetime);
            var isClicked = false;

            _ui.Clicked.Listen(stepLifetime, () => isClicked = true);
            return UniTask.WaitUntil(() => isClicked == true);
        }
    }
}