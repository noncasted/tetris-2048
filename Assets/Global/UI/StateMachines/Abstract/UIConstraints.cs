using System.Collections.Generic;
using Global.Inputs.Constraints.Abstract;

namespace Global.UI.StateMachines.Abstract
{
    public class UIConstraints : IUIConstraints
    {
        public UIConstraints()
        {
            Input = new Dictionary<InputConstraints, bool>();
        }
        
        public UIConstraints(InputConstraints input)
        {
            Input = new Dictionary<InputConstraints, bool>()
            {
                { input, false }
            };
        }

        public UIConstraints(IReadOnlyDictionary<InputConstraints, bool> input)
        {
            Input = input;
        }

        public IReadOnlyDictionary<InputConstraints, bool> Input { get; }
    }
}