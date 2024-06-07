using Common.DataTypes.Runtime.Reactive;
using Global.System.MessageBrokers.Abstract;
using Global.UI.Design.Runtime.Buttons;
using Internal.Scopes.Abstract.Lifetimes;
using Internal.Services.Options.Implementations;
using UnityEngine;

namespace Features.Tutorial.Runtime.Steps.SpeedModifications
{
    public class TutorialStep_SpeedModifications_UI : TutorialStepUI
    {
        [SerializeField] private TutorialSwitchKey _outlineSwitchKey;
        
        [SerializeField] private DesignButton _button;
        [SerializeField] private GameObject _desktop;
        [SerializeField] private GameObject _mobile;

        public IViewableDelegate Clicked => _button.Clicked;
        
        public void OnEntered(PlatformOptions options, IReadOnlyLifetime stepLifetime)
        {
            if (options.IsMobile == true)
            {
                _desktop.SetActive(false);
                _mobile.SetActive(true);
            }
            else
            {
                _desktop.SetActive(true);
                _mobile.SetActive(false);
            }
            
            Msg.Publish(new TutorialEnableEvent(_outlineSwitchKey));
            stepLifetime.ListenTerminate(() => Msg.Publish(new TutorialDisableEvent(_outlineSwitchKey)));
        }
    }
}