using Common.DataTypes.Runtime.Reactive;
using UnityEngine;

namespace Global.UI.Design.Abstract.Layouts
{
    public abstract class BaseDesignLayoutElement : MonoBehaviour
    {
        public abstract IViewableProperty<float> Height { get; }

        public abstract void BindChild(BaseDesignLayoutElement child);
        public abstract void ForceRecalculate();
    }
}