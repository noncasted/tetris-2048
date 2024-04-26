using System.Collections.Generic;
using Global.UI.EventSystems.Runtime;
using Global.UI.LoadingScreens.Runtime;
using Global.UI.Localizations.Runtime;
using Global.UI.Overlays.Runtime;
using Global.UI.StateMachines.Runtime;
using Internal.Scopes.Abstract.Instances.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.UI.Compose
{
    [InlineEditor]
    public class GlobalUICompose : ScriptableObject, IServicesCompose
    {
        [SerializeField] private LoadingScreenFactory _loadingScreen;
        [SerializeField] private LocalizationFactory _localization;
        [SerializeField] private GlobalOverlayFactory _globalOverlay;
        [SerializeField] private UiStateMachineFactory _uiStateMachine;
        [SerializeField] private EventSystemFactory _eventSystem;

        public IReadOnlyList<IServiceFactory> Factories => new IServiceFactory[]
        {
            _loadingScreen,
            _localization,
            _globalOverlay,
            _uiStateMachine,
            _eventSystem
        };
    }
}