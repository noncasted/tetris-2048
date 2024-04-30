using LeTai.Asset.TranslucentImage;
using UnityEngine;
using VContainer;

namespace Menu.Common
{
    public class TranslucentImageUpdater : MonoBehaviour
    {
        [SerializeField] private TranslucentImage _image;
        [SerializeField] private ScalableBlurConfig _config;
        
        [SerializeField] [Min(0f)] private float _strength;
        [SerializeField] [Min(0f)] private float _time;

        private bool _isActive;

        private float _progressTime;
        private int _direction;

        public bool IsActive => _isActive;

        [Inject]
        private void Construct(TranslucentImageSource imageSource)
        {
            _config.Strength = 0f;
            _image.enabled = false;
            _image.source = imageSource;
        }
        
        public void Show()
        {
            _image.enabled = true;
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
            _config.Strength = progress * _strength;
            
            if ((_direction == 1 && progress < 1f) || (_direction == -1 && progress > 0f))
                return;

            _isActive = false;
            
            if (_direction == -1)
                _image.enabled = false;
        }
    }
}