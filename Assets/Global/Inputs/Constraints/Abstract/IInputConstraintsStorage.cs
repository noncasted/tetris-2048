using System.Collections.Generic;

namespace Global.Inputs.Constraints.Abstract
{
    public interface IInputConstraintsStorage
    {
        bool this[InputConstraints key] { get; }

        void Add(IReadOnlyDictionary<InputConstraints, bool> constraint);
        void Remove(IReadOnlyDictionary<InputConstraints, bool> constraint);
    }
}