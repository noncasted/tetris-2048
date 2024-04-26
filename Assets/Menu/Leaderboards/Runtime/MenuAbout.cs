using Global.Inputs.Constraints.Abstract;
using Global.UI.StateMachines.Abstract;
using Menu.Common;
using Menu.Leaderboards.Abstract;
using UnityEngine;

namespace Menu.Leaderboards.Runtime
{
    [DisallowMultipleComponent]
    public class MenuAbout : MonoBehaviour, IMenuAbout
    {
        [SerializeField] private MenuStateTransition _transition;

        public IUIConstraints Constraints => new UIConstraints(InputConstraints.Game);

        public string Name => "Menu_Settings";

        public void Enter(IStateHandle handle)
        {
            _transition.Transit();
            handle.VisibilityLifetime.ListenTerminate(_transition.Exit);
        }
    }
}