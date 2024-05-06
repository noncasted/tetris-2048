using Features.Menu.Common;
using Features.Menu.Leaderboards.Abstract;
using Global.Inputs.Constraints.Abstract;
using Global.UI.StateMachines.Abstract;
using Internal.Scopes.Abstract.Options;
using Internal.Services.Options.Implementations;
using UnityEngine;
using VContainer;

namespace Features.Menu.Leaderboards.Runtime
{
    [DisallowMultipleComponent]
    public class MenuAbout : MonoBehaviour, IMenuAbout
    {
        [SerializeField] private MenuStateTransition _transition;
        [SerializeField] private PlatformDependentObjects _body;

        public IUIConstraints Constraints => new UIConstraints(InputConstraints.Game);

        public string Name => "Menu_Settings";

        [Inject]
        private void Construct(IOptions options)
        {
            var platformOptions = options.GetOptions<PlatformOptions>();

            _body.SetPlatform(platformOptions);
        }

        public void OnEntered(IStateHandle handle)
        {
            _transition.Transit();
            handle.VisibilityLifetime.ListenTerminate(_transition.Exit);
        }
    }
}