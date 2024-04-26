using System;
using Common.DataTypes.Runtime.Reactive;
using Global.UI.Design.Abstract.Buttons;
using Global.UI.Design.Abstract.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace Global.UI.Design.Runtime.Buttons
{
    [RequireComponent(typeof(Image))]
    public class DesignElementColor : DesignElementBehaviour
    {
        [SerializeField] private BaseElementConfig _config;
        [SerializeField] private Image _image;

        private float _currentTransitionTime;
        private Color _targetColor;
        private Color _fromColor;

        public override void Construct(IDesignElement element)
        {
            element.State.View(element.Lifetime, OnStateChanged);
        }

        private void Update()
        {
            var progress = _currentTransitionTime / _config.TransitionTime;
            progress = Mathf.Clamp01(progress);

            var color = Color.Lerp(_fromColor, _targetColor, progress);
            _image.color = color;

            _currentTransitionTime += Time.deltaTime;
        }

        private void OnStateChanged(DesignElementState state)
        {
            var color = state switch
            {
                DesignElementState.Idle => _config.Idle,
                DesignElementState.Hovered => _config.Hovered,
                DesignElementState.Pressed => _config.Pressed,
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
            };

            _fromColor = _image.color;
            _targetColor = color;
            _currentTransitionTime = 0f;
        }

        private void OnDrawGizmosSelected()
        {
            GetComponent<Image>().color = _config.Idle;
        }

        private void OnValidate()
        {
            if (_image == null)
                _image = GetComponent<Image>();
        }
    }
}