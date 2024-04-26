using Common.DataTypes.Editor.Collections;
using Global.Inputs.Constraints.Abstract;
using UnityEditor;

namespace Global.Inputs.Constraints.Editor
{
    [CustomPropertyDrawer(typeof(InputConstraintsDictionary))]
    public class InputConstraintsDictionaryDrawer : ReadOnlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}