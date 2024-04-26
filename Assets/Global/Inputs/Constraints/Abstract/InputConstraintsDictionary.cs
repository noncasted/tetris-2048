using System;
using System.Collections.Generic;
using Common.DataTypes.Runtime.Collections;

namespace Global.Inputs.Constraints.Abstract
{
    [Serializable]
    public class InputConstraintsDictionary : ReadOnlyDictionary<InputConstraints, bool>
    {
        public IReadOnlyDictionary<InputConstraints, bool> Value => this;
    }
}