using UnityEngine;

namespace Features.GamePlay.Loop.Runtime
{
    [DisallowMultipleComponent]
    public class GamePlayCamera : MonoBehaviour
    {
        private const float _fullHdFactor = 1920f / 1080f;
        
        [SerializeField] private float _baseScale = 4f;
        [SerializeField] private Camera _camera;

        private void Update()
        {
            var baseSize = _baseScale / _fullHdFactor;
            var currentFactor = GetCurrentFactor();
            _camera.orthographicSize = baseSize * currentFactor;
            
            float GetCurrentFactor()
            {
                if (Screen.width > Screen.height)
                    return (float)Screen.width / Screen.height;
                
                return (float)Screen.height / Screen.width;
            }
        }
    }
}