using Common.DataTypes.Runtime.Reactive;
using Features.Menu.Common;
using Features.Menu.GameEnds.Abstract;
using Global.Inputs.Constraints.Abstract;
using Global.UI.Design.Runtime.Buttons;
using Global.UI.StateMachines.Abstract;
using TMPro;
using UnityEngine;

namespace Features.Menu.GameEnds.Runtime
{
    public class MenuGameEnd : MonoBehaviour, IMenuGameEnd
    {
        [SerializeField] private MenuStateTransition _transition;
        [SerializeField] private DesignButton _exitButton;
        [SerializeField] private TMP_Text _currentScore;

        private readonly UIConstraints _constraints = new(InputConstraints.Game);
        private readonly ViewableDelegate _exitRequested = new();
        public IUIConstraints Constraints => _constraints;
        public string Name => "GameEnd";

        public IViewableDelegate ExitRequested => _exitRequested;

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
            _exitRequested.Invoke();
        }
    }
}