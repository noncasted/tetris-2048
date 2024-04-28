using Common.DataTypes.Runtime.Reactive;
using Global.Inputs.Constraints.Abstract;
using Global.UI.Design.Runtime.Buttons;
using Global.UI.StateMachines.Abstract;
using Menu.Common;
using Menu.GameEnds.Abstract;
using TMPro;
using UnityEngine;

namespace Menu.GameEnds.Runtime
{
    public class MenuGameEnd : MonoBehaviour, IMenuGameEnd
    {
        [SerializeField] private MenuStateTransition _transition;
        [SerializeField] private DesignButton _exitButton;
        [SerializeField] private TMP_Text _currentScore;

        private readonly UIConstraints _constraints = new(InputConstraints.Game);
        private readonly ViewableDelegate _exitClicked = new();
        public IUIConstraints Constraints => _constraints;
        public string Name => "GameEnd";

        public IViewableDelegate ExitRequested => _exitClicked;

        public void Show(IStateHandle handle, int currentScore)
        {
            _transition.Transit();
            handle.HierarchyLifetime.ListenTerminate(_transition.Exit);
            _exitButton.Clicked.Listen(handle.HierarchyLifetime, OnExitClicked);
            _currentScore.text = currentScore.ToString();
        }

        public void OnEntered(IStateHandle handle)
        {
        }

        private void OnExitClicked()
        {
            _exitClicked.Invoke();
        }
    }
}