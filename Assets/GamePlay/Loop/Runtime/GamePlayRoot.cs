using Global.Cameras.CurrentProvider.Abstract;
using Internal.Scopes.Abstract.Instances.Services;
using UnityEngine;
using VContainer;

namespace GamePlay.Loop.Runtime
{
    [DisallowMultipleComponent]
    public class GamePlayRoot : MonoBehaviour, IScopeAwakeListener
    {
        [SerializeField] private Camera _camera;
        
        private ICurrentCameraProvider _cameraProvider;

        [Inject]
        private void Inject(ICurrentCameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
        }

        public void OnAwake()
        {
            _cameraProvider.SetCamera(_camera);
        }
    }
}