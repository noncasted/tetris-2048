using System.Collections.Generic;
using Global.Inputs.Constraints.Abstract;
using Global.UI.StateMachines.Abstract;
using UnityEngine;

namespace Global.UI.StateMachines.Runtime
{
    public class UiConstraintsAsset : ScriptableObject, IUIConstraints
    {
        [SerializeField] private InputConstraintsDictionary _input;

        public IReadOnlyDictionary<InputConstraints, bool> Input => _input.Value;
    }
}