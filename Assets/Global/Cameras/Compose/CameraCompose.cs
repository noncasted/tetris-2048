using System.Collections.Generic;
using Global.Cameras.CurrentProvider.Runtime;
using Global.Cameras.Persistent.Runtime;
using Global.Cameras.Utils.Runtime;
using Internal.Scopes.Abstract.Instances.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Cameras.Compose
{
    [InlineEditor]
    public class CameraCompose : ScriptableObject, IServicesCompose
    {
        [SerializeField] private CurrentCameraProviderFactory _currentProvider;
        [SerializeField] private GlobalCameraFactory _global;
        [SerializeField] private CameraUtilsFactory _utils;

        public IReadOnlyList<IServiceFactory> Factories => new IServiceFactory[]
        {
            _currentProvider,
            _global,
            _utils
        };
    }
}