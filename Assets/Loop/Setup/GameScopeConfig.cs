using System;
using System.Collections.Generic;
using Common.DataTypes.Runtime.Attributes;
using GamePlay.Loop.Runtime;
using GamePlay.Overlay.Runtime;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Scenes;
using Internal.Scopes.Common.Services;
using Loop.Runtime;
using Menu.Loop.Runtime;
using Sirenix.OdinInspector;
using Tutorial.Runtime;
using UnityEngine;
using VContainer.Unity;

namespace Loop.Setup
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
            _serviceDefaultCallbacks
        };

        public IReadOnlyList<IServicesCompose> Composes => Array.Empty<IServicesCompose>();
    }
}