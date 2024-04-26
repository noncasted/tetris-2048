using System.Collections.Generic;
using Global.Inputs.Constraints.Abstract;

namespace Global.UI.StateMachines.Abstract
{
    public interface IUIConstraints
    {
        IReadOnlyDictionary<InputConstraints, bool> Input { get; }
    }
}