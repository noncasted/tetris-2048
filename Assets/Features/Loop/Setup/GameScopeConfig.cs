using System;
using System.Collections.Generic;
using Common.DataTypes.Runtime.Attributes;
using Features.GamePlay.Loop.Runtime;
using Features.GamePlay.Overlay.Runtime;
using Features.Loop.Runtime;
using Features.Loop.Sounds.Runtime;
using Features.Menu.Loop.Runtime;
using Features.Tutorial.Runtime;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Scenes;
using Internal.Scopes.Common.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer.Unity;

namespace Features.Loop.Setup
{
    [InlineEditor]
    [CreateAssetMenu(fileName = "GameScopeConfig", menuName = "Game/ScopeConfig")]
    public class GameScopeConfig : ScriptableObject, IServiceScopeConfig
    {
        [SerializeField] [CreateSO] private GameLoopFactory _gameLoop;
        [SerializeField] [CreateSO] private GamePlayLoopFactory _gamePlayLoop;
        [SerializeField] [CreateSO] private MenuLoopFactory _menuLoop;
        [SerializeField] [CreateSO] private OverlayFactory _overlay;
        [SerializeField] [CreateSO] private TutorialFactory _tutorial;
        [SerializeField] [CreateSO] private GameSoundsFactory _sounds;
        
        [SerializeField] private ServiceDefaultCallbacksFactory _serviceDefaultCallbacks;

        [SerializeField] private GameScope _scope;
        [SerializeField] [CreateSO] private SceneData _servicesScene;
        [SerializeField] private bool _isMock;

        public LifetimeScope ScopePrefab => _scope;
        public SceneData ServicesScene => _servicesScene;
        public bool IsMock => _isMock;

        public IReadOnlyList<IServiceFactory> Services => new IServiceFactory[]
        {
            _gameLoop,
            _gamePlayLoop,
            _menuLoop,
            _overlay,
            _tutorial,
            _sounds,
            _serviceDefaultCallbacks
        };

        public IReadOnlyList<IServicesCompose> Composes => Array.Empty<IServicesCompose>();
    }
}