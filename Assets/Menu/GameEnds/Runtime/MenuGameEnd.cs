using Global.Inputs.Constraints.Abstract;
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
        [SerializeField] private TMP_Text _currentScore;

        private readonly UIConstraints _constraints = new(InputConstraints.Game);

        public IUIConstraints Constraints => _constraints;
        public string Name => "GameEnd";

        public void Show(IStateHandle handle, int currentScore)
        {
            _transition.Transit();
            handle.HierarchyLifetime.ListenTerminate(_transition.Exit);
            _currentScore.text = currentScore.ToString();
        }

        public void Enter(IStateHandle handle)
        {
        }
    }
}