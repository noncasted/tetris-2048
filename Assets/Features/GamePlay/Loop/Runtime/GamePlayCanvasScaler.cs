using UnityEngine;
using UnityEngine.UI;

namespace Features.GamePlay.Loop.Runtime
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CanvasScaler))]
    public class GamePlayCanvasScaler : MonoBehaviour
    {
        private const float _fullHdFactor = 1920f / 1080f;

        private CanvasScaler _scaler;

        private void Awake()
        {
            _scaler = GetComponent<CanvasScaler>();
        }

        private void Update()
        {
            if (Screen.width > Screen.height)
            {
                _scaler.referenceResolution = new Vector2(1080, 1920);
                return;
            }

            var resolution = new Vector2(Screen.width, Screen.height);

            while (resolution.x < 1000 || resolution.y < 1000)
                resolution *= 1.5f;

            _scaler.referenceResolution = resolution;
        }
    }
}