using UnityEngine;

namespace Global.UI.Design.Abstract.Elements
{
    public abstract class BaseElementConfig : ScriptableObject
    {
        public abstract Color Idle { get; }
        public abstract Color Hovered { get; }
        public abstract Color Pressed { get; }
        
        public abstract float TransitionTime { get; }
    }
}