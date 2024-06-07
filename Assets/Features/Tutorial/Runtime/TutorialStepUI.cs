using Features.Menu.Common;
using Internal.Scopes.Abstract.Lifetimes;
using UnityEngine;

namespace Features.Tutorial.Runtime
{
    public class TutorialStepUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroupUpdater _canvasGroup;

        public void Enter(IReadOnlyLifetime stepLifetime)
        {
            gameObject.SetActive(true);
            _canvasGroup.Show();
            
            stepLifetime.ListenTerminate(() =>
            {
                _canvasGroup.Hide();
                gameObject.SetActive(false);
            });
        }
    }
}