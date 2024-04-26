using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GamePlay.Boards.Abstract.Blocks
{
    [InlineEditor]
    public class BlockColors : SerializedScriptableObject
    {
        [SerializeField] private Dictionary<int, Color> _colors;

        public Color GetColor(int value)
        {
            if (_colors.TryGetValue(value, out var color) == false)
                color = _colors[_colors.Keys.Max()];

            return color; 
        }

    }
}