using Global.Cameras.CurrentProvider.Abstract;
using Internal.Scopes.Abstract.Instances.Services;
using UnityEngine;
using VContainer;

namespace Tutorial.Runtime
{
    public class TutorialUI : MonoBehaviour, IScopeEnableListener
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