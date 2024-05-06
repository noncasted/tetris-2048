using UnityEngine;

namespace Features.Menu.Common
{
    [DisallowMultipleComponent]
    public class MenuStateTransition : MonoBehaviour
    {
        [SerializeField] private CanvasGroupUpdater _canvasGroup;
        [SerializeField] private TranslucentImageUpdater _translucentImage;

        private bool _isActive;
        
        public void Transit()
        {
            _isActive = true;
            
            gameObject.SetActive(true);
            _translucentImage.Show();
            _canvasGroup.Show();
        }

        public void Exit()
        {
            _translucentImage.Hide();
            _canvasGroup.Hide();
            _isActive = false;
        }

        private void Update()
        {
            if (_translucentImage.IsActive == true || _canvasGroup.IsActive == true)
                return;
            
            if (_isActive == true)
                return;
            
            gameObject.SetActive(false);
        }
    }
}