using Global.Cameras.CurrentProvider.Abstract;
using Internal.Scopes.Abstract.Instances.Services;
using UnityEngine;
using VContainer;

namespace Features.GamePlay.Overlay.Runtime
{
    [DisallowMultipleComponent]
    public class Overlay : MonoBehaviour, IScopeEnableListener
    {
        [SerializeField] private Canvas _canvas;
        
        private ICurrentCameraProvider _cameraProvider;

        [Inject]
        private void Inject(ICurrentCameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
        }

        public void OnEnabled()
        {
            _canvas.worldCamera = _cameraProvider.Current;
        }
    }
}