using NaughtyAttributes;
using UnityEngine;

namespace Features.GamePlay.Overlay.Runtime
{
    [DisallowMultipleComponent]
    public class OverlaySpeedSelection : MonoBehaviour
    {
        [SerializeField] private float _time;
        [SerializeField] [CurveRange(0f, 0f, 1f, 1f)] private AnimationCurve _moveCurve;
        [SerializeField] [CurveRange(0f, 0f, 1f, 2f)] private AnimationCurve _scaleCurve;

        private float _progressTime;
        private Transform _target;
        private Vector3 _startPosition;

        private void Awake()
        {
            _startPosition = transform.position;
        }

        public void MoveTo(Transform target)
        {
            _startPosition = transform.position;
            _target = target;
            _progressTime = 0f;
        }

        private void Update()
        {
            if (_target == null || _progressTime > _time)
                return;

            var self = transform;
            _progressTime += Time.deltaTime;            
            var progress = _progressTime / _time;

            var moveValue = _moveCurve.Evaluate(progress);
            var position = Vector2.Lerp(_startPosition, _target.position, moveValue);
            self.position = position;
            
            var scaleValue = _scaleCurve.Evaluate(progress);
            self.localScale = Vector3.one * scaleValue;
        }
    }
}