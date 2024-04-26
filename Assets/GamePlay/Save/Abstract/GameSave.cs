using System;
using System.Collections.Generic;
using Global.Publisher.Abstract.DataStorages;

namespace GamePlay.Save.Abstract
{
    [Serializable]
    public class GameSave
    {
        public GameSave()
        {
            Tiles = new List<BoardStateBlock>();
            MaxScore = 0;
        }
        
        public GameSave(List<BoardStateBlock> tiles, int maxScore)
        {
            Tiles = tiles;
            MaxScore = maxScore;
        }
        
        public readonly List<BoardStateBlock> Tiles;
        public readonly int MaxScore;

        public int GetScore()
        {
            var score = 0;

            foreach (var block in Tiles)
                score += block.Value;

            return score;
        }
    }

    public class BoardSaveSerializer : StorageEntrySerializer<GameSave>
    {
        public BoardSaveSerializer() : base("board", new GameSave())
        {
        }
    }
}