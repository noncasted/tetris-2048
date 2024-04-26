using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.BlockSpawners.Runtime
{
    public class BlockSpawnerConfig : ScriptableObject
    {
        [SerializeField] private int[] _valuesProbability = { 2, 2, 2, 4 };
        [SerializeField] [Min(0f)] private float _spawnRate;

        public IReadOnlyList<int> ValuesProbability => _valuesProbability;
        public float SpawnRate => _spawnRate;
    }
}