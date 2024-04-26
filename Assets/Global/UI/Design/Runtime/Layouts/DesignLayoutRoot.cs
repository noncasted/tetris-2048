using Global.UI.Design.Abstract.Extensions;
using Global.UI.Design.Abstract.Layouts;
using UnityEngine;

namespace Global.UI.Design.Runtime.Layouts
{
    [DisallowMultipleComponent]
    public class DesignLayoutRoot : MonoBehaviour
    {
        public void ForceRecalculate()
        {
            var children = this.GetComponentInChildOnlyIncludeSelf<BaseDesignLayoutElement>();

            foreach (var child in children)
                child.ForceRecalculate();
        }

        private void OnDrawGizmos()
        {
            ForceRecalculate();
        }
    }
}