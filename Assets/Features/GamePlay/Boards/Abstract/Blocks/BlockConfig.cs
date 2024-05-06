using NaughtyAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.GamePlay.Boards.Abstract.Blocks
{
    [InlineEditor]
    public class BlockConfig : ScriptableObject
    {
        [SerializeField] [Min(0f)] private float _moveTime;

        [SerializeField] [CurveRange(0f, 0f, 1f, 1f)]
        private AnimationCurve _moveCurve;
        
        public float MoveTime => _moveTime;
        public AnimationCurve MoveCurve => _moveCurve;
    }
}