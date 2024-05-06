using Shapes;
using UnityEngine;

namespace Features.GamePlay.Boards.Runtime.Blocks.Moving
{
    [DisallowMultipleComponent]
    public class MovingBlockAppearance : MonoBehaviour
    {
        [SerializeField] private ShapeRenderer _shape;
        [SerializeField] private float _time = 1f;

        private float _currentTime;

        private void OnEnable()
        {
            _currentTime = 0f;
        }

        private void Update()
        {
            if (_currentTime > _time)
                return;
            
            _currentTime += Time.deltaTime;
            var progress = _currentTime / _time;

            var color = _shape.Color;
            color.a = progress;
            _shape.Color = color;
        }
    }
}