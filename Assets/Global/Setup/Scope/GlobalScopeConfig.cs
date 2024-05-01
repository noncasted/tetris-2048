using System.Collections.Generic;
using Common.DataTypes.Runtime.Attributes;
using Global.Audio.Compose;
using Global.Backend.Runtime;
using Global.Cameras.Compose;
using Global.Configs.Compose;
using Global.Debugs.Drawing.Runtime;
using Global.GameLoops.Runtime;
using Global.Inputs.Compose;
using Global.Publisher.Setup;
using Global.System.Compose;
using Global.UI.Compose;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Scenes;
using Internal.Scopes.Common.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer.Unity;

namespace Global.Setup.Scope
{
    [InlineEditor]
    [CreateAssetMenu(fileName = "GlobalConfig", menuName = "Global/Config")]
    public class GlobalScopeConfig : ScriptableObject, IServiceScopeConfig
    {
        [SerializeField] [CreateSO] private AudioCompose _audio;
        [SerializeField] [CreateSO] private CameraCompose _camera;
        [SerializeField] [CreateSO] private InputCompose _input;
        [SerializeField] [CreateSO] private SystemCompose _system;
        [SerializeField] [CreateSO] private GlobalUICompose _ui;
        [SerializeField] [CreateSO] private GlobalLoopFactory _globalLoop;
        [SerializeField] [CreateSO] private PublisherSetup _publisher;
        [SerializeField] [CreateSO] private BackendFactory _backend;
        [SerializeField] [CreateSO] private ServiceDefaultCallbacksFactory _serviceDefaultCallbacks;
        [SerializeField] [CreateSO] private ConfigsCompose _configs;
        [SerializeField] [CreateSO] private DrawingFactory _drawing;
        
        [SerializeField] private GlobalScope _scope;
        [SerializeField] private SceneData _servicesScene;
        [SerializeField] private bool _isMock;
        
        public LifetimeScope ScopePrefab => _scope;
        public SceneData ServicesScene => _servicesScene;
        public bool IsMock => _isMock;
        public IReadOnlyList<IServiceFactory> Services => GetFactories();

        public IReadOnlyList<IServicesCompose> Composes => new IServicesCompose[]
        {
            _camera,
            _audio,
            _input,
            _system,
            _ui,
            _configs
        };

        private IServiceFactory[] GetFactories() => new IServiceFactory[]
        {
            _publisher,
            _globalLoop,
            _backend,
            _serviceDefaultCallbacks,
            _drawing
        };
    }
}