using Common.DataTypes.Runtime.Reactive;
using Global.UI.Design.Runtime.Buttons;
using UnityEngine;

namespace Tutorial.Runtime.Steps.SpeedModifications
{
    public class TutorialStep_SpeedModifications_UI : TutorialStepUI
    {
        [SerializeField] private DesignButton _button;

        public IViewableDelegate Clicked => _button.Clicked;
    }
}