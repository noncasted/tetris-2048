using System;
using UnityEngine;

namespace GamePlay.Save.Abstract
{
    [Serializable]
    public class BoardStateBlock
    {
        public BoardStateBlock(Vector2Int position, int value)
        {
            Position = position;
            Value = value;
        }

        public readonly Vector2Int Position;
        public readonly int Value;
    }
}