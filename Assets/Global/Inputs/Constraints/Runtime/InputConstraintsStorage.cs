using System;
using System.Collections.Generic;
using System.Linq;
using Global.Inputs.Constraints.Abstract;

namespace Global.Inputs.Constraints.Runtime
{
    public class InputConstraintsStorage : IInputConstraintsStorage
    {
        public InputConstraintsStorage()
        {
            var constraints = Enum.GetValues(typeof(InputConstraints)).Cast<InputConstraints>();

            foreach (var constraint in constraints)
                _constraints.Add(constraint, 0);
        }

        private readonly Dictionary<InputConstraints, int> _constraints = new();

        public bool this[InputConstraints key] => _constraints[key] <= 0;

        public void Add(IReadOnlyDictionary<InputConstraints, bool> constraints)
        {
            foreach (var (key, value) in constraints)
            {
                if (value == false)
                    continue;

                _constraints[key]++;
            }
        }

        public void Remove(IReadOnlyDictionary<InputConstraints, bool> constraints)
        {
            foreach (var (key, value) in constraints)
            {
                if (value == false)
                    continue;

                _constraints[key]--;

                var count = _constraints[key];

                if (count < 0)
                    _constraints[key] = 0;
            }
        }
    }
}