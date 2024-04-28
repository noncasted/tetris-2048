using Internal.Scopes.Abstract.Lifetimes;
using Menu.Common;
using UnityEngine;

namespace Tutorial.Runtime
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