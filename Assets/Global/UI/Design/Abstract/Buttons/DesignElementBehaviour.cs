using Global.UI.Design.Abstract.Elements;
using UnityEngine;

namespace Global.UI.Design.Abstract.Buttons
{
    public abstract class DesignElementBehaviour : MonoBehaviour
    {
        public abstract void Construct(IDesignElement root);
    }
}