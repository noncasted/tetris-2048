using Features.GamePlay.Boards.Runtime.Blocks.Moving;
using Features.GamePlay.Boards.Runtime.Blocks.Static;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.GamePlay.Boards.Runtime.Factory
{
    [InlineEditor]
    public class BlockFactoryConfig : SerializedScriptableObject
    {
        [SerializeField] private StaticBlock _static;
        [SerializeField] private MovingBlock _moving;

        public StaticBlock Static => _static;
        public MovingBlock Moving => _moving;
    }
 }