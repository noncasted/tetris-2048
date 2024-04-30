using Internal.Services.Options.Implementations;
using UnityEngine;

namespace Tutorial.Runtime.Steps.MoveAndCombine
{
    [DisallowMultipleComponent]
    public class TutorialStep_MoveAndCombine_UI : TutorialStepUI
    {
        [SerializeField] private GameObject _desktop;
        [SerializeField] private GameObject _mobile;
        
        public void SetPlatform(PlatformOptions options)
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
        }
    }
}