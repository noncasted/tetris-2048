using Global.UI.Design.Abstract.Elements;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.UI.Design.Runtime.Elements
{
    [InlineEditor]
    [CreateAssetMenu(fileName = "NestedElementConfig", menuName = "UI/Design/ElementConfig/Nested")]
    public class NestedElementConfig : BaseElementConfig
    {
        [SerializeField] private Color _idle;
        [SerializeField] private BaseElementConfig _source;

        public override Color Idle => _idle;
        public override Color Hovered => GetHover();
        public override Color Pressed => GetPressed();
        
        public override float TransitionTime => _source.TransitionTime;

        private Color GetHover()
        {
            var color = _source.Idle - _source.Hovered;
            Debug.Log($"Get hover: {_source.Idle} - {_source.Hovered} = {color} {_idle} -> {_idle - color}");
            return _idle - color;
        }
        
        private Color GetPressed()
        {
            var color = _source.Hovered - _source.Pressed;
            Debug.Log($"Get pressed: {_source.Hovered} - {_source.Pressed} = {color} {_idle} -> { _idle - color}");
            return _idle - color;
        }
    }
}