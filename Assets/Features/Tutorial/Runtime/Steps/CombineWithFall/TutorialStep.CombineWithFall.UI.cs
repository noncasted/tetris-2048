using UnityEngine;

namespace Features.Tutorial.Runtime.Steps.CombineWithFall
{
    [DisallowMultipleComponent]
    public class TutorialStep_CombineWithFall_UI : TutorialStepUI
    {
        [SerializeField] private GameObject _fallTip;
        [SerializeField] private GameObject _combineTip;

        public void ShowFall()
        {
            _fallTip.SetActive(true);
            _combineTip.SetActive(false);
        }
        
        public void ShowCombine()
        {
            _fallTip.SetActive(false);
            _combineTip.SetActive(true);
        }
    }
}