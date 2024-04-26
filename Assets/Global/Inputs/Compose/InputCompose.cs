using System.Collections.Generic;
using Global.Inputs.Constraints.Runtime;
using Global.Inputs.Utils.Runtime;
using Global.Inputs.View.Runtime;
using Internal.Scopes.Abstract.Instances.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Inputs.Compose
{
    [InlineEditor]
    public class InputCompose : ScriptableObject, IServicesCompose
    {
        [SerializeField] private InputConstraintsFactory _constraints;
        [SerializeField] private InputUtilsFactory _utils;
        [SerializeField] private InputViewFactory _view;

        public IReadOnlyList<IServiceFactory> Factories => new IServiceFactory[]
        {
            _constraints,
            _utils,
            _view
        };
    }
}