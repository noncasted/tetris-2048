using Common.Components.Runtime.ObjectLifetime;
using Features.GamePlay.Boards.Abstract.Blocks;
using NaughtyAttributes;
using UnityEngine;

namespace Features.GamePlay.Boards.Runtime.Blocks.Static
{
    [DisallowMultipleComponent]
    public class StaticBlockTweens : MonoBehaviour
    {
        [SerializeField] private float _moveTime;
        [SerializeField] private float _moveDistance;
        [SerializeField] [CurveRange(0f, 0f, 1f, 1f)] private AnimationCurve _moveCurve;
        
        [SerializeField] private StaticBlock _block;

        private float _currentTime = float.PositiveInfinity;
        private Vector2 _startPosition;
        private Vector2 _targetPosition;

        private void OnEnable()
        {
            var lifetime = this.GetObjectLifetime();
            _block.Value.UpgradeStarted.Listen(lifetime, OnUpgradeStarted);
        }

        private void OnUpgradeStarted(int _, BlockUpgradeData data)
        {
            _currentTime = 0f;
            _startPosition = transform.localPosition;
            _targetPosition = _startPosition + data.Direction * _moveDistance * -1f;
        }

        private void Update()
        {
            if (_currentTime > _moveTime)
            {
                transform.localPosition = Vector3.zero;
                return;
            }

            _currentTime += Time.deltaTime;
            var progress = _currentTime / _moveTime;
            var factor = _moveCurve.Evaluate(progress);
            var position = Vector2.Lerp(_startPosition, _targetPosition, factor);
            transform.localPosition = position;
        }
    }
}