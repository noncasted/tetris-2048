using Common.Components.Runtime.ObjectLifetime;
using GamePlay.Boards.Abstract.Blocks;
using NaughtyAttributes;
using Shapes;
using UnityEngine;

namespace GamePlay.Boards.Runtime.Blocks.Static
{
    [DisallowMultipleComponent]
    public class UpgradeVfx : MonoBehaviour
    {
        [SerializeField] private float _time;
        [SerializeField] private float _targetScale;
        
        [SerializeField] private float _startAlpha;
        [SerializeField] private float _endAlpha;

        [SerializeField] [CurveRange(0f, 0f, 1f, 1f)]
        private AnimationCurve _alphaCurve;

        [SerializeField] [CurveRange(0f, 0f, 1f, 1f)]
        private AnimationCurve _scaleCurve;
        
        [SerializeField] private StaticBlock _block;
        [SerializeField] private ShapeRenderer _shape;
        [SerializeField] private BlockColors _colors;

        private float _progressTime = 0f;

        private void OnEnable()
        {
            var lifetime = this.GetObjectLifetime();
            _block.Value.UpgradeStarted.Listen(lifetime, OnUpgradeStarted);
        }

        private void OnUpgradeStarted(int value)
        {
            _progressTime = 0f;
            _shape.enabled = true;
            var color = _colors.GetColor(value);
            _shape.Color = color;
        }

        private void Update()
        {
            if (_progressTime > _time)
            {
                _shape.enabled = false;
                return;
            }
            
            _progressTime += Time.deltaTime;
            var progress = _progressTime / _time;
            var scaleFactor = _scaleCurve.Evaluate(progress);
            transform.localScale = Vector3.one * scaleFactor * _targetScale;

            var alphaFactor = _alphaCurve.Evaluate(progress);
            var alpha = Mathf.Lerp(_startAlpha, _endAlpha, alphaFactor);
            var color = _shape.Color;
            color.a = alpha;
            _shape.Color = color;
            _shape.UpdateMesh();
        }
    }
}