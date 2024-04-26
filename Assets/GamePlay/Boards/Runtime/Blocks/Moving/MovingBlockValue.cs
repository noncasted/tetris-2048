using GamePlay.Boards.Abstract.Blocks;
using Shapes;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace GamePlay.Boards.Runtime.Blocks.Moving
{
    public class MovingBlockValue : MonoBehaviour
    {
        [SerializeField] private ShapeRenderer _shape;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private BlockColors _colors;

        [SerializeField] [ReadOnly] private int _number;

        public int Number => _number;

        public void Construct(int value)
        {
            _text.text = value.ToString();
            _shape.Color = _colors.GetColor(value);
            _number = value;
        }
    }
}