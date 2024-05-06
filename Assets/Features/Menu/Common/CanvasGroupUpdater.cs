using UnityEngine;

namespace Features.Menu.Common
{
    public class CanvasGroupUpdater : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] [Min(0f)] private float _time;

        private bool _isActive;

        private float _progressTime;
        private int _direction;

        public bool IsActive => _isActive;

        private void Awake()
        {
            _canvasGroup.alpha = 0;
        }

        public void Show()
        {
            _direction = 1;
            _isActive = true;
        }

        public void Hide()
        {
            _direction = -1;
            _isActive = true;
        }

        private void Update()
        {
            if (_isActive == false)
                return;

            _progressTime += Time.deltaTime * _direction;

            var progress = _progressTime / _time;
            _canvasGroup.alpha = progress;

            if ((_direction == 1 && progress < 1f) || (_direction == -1 && progress > 0f))
                return;

            _isActive = false;
        }
    }
}