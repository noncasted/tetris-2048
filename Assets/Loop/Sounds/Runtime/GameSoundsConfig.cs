using Sirenix.OdinInspector;
using UnityEngine;

namespace Loop.Sounds.Runtime
{
    [InlineEditor]
    public class GameSoundsConfig : ScriptableObject
    {
        [SerializeField] private AudioClip _background;
        [SerializeField] private AudioClip _buttonClick;
        [SerializeField] private AudioClip _blockMove;
        [SerializeField] private AudioClip _blockCombine;

        public AudioClip Background => _background;
        public AudioClip ButtonClick => _buttonClick;
        public AudioClip BlockMove => _blockMove;
        public AudioClip BlockCombine => _blockCombine;
    }
}