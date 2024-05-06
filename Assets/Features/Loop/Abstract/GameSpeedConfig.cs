using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Loop.Abstract
{
    [InlineEditor]
    public class GameSpeedConfig : SerializedScriptableObject
    {
        [SerializeField] private Dictionary<GameSpeed, float> _speeds;

        public IReadOnlyDictionary<GameSpeed, float> Speeds => _speeds;
    }
}