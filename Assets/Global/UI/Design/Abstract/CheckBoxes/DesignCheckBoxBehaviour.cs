using Internal.Scopes.Abstract.Lifetimes;
using UnityEngine;

namespace Global.UI.Design.Abstract.CheckBoxes
{
    public abstract class DesignCheckBoxBehaviour : MonoBehaviour
    {
        public abstract void Construct(IDesignCheckBox checkBox, IReadOnlyLifetime lifetime);
    }
}