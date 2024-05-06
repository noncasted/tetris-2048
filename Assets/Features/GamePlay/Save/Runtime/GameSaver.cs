using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Features.GamePlay.Loop.Scores.Abstract;
using Features.GamePlay.Save.Abstract;
using Global.Publisher.Abstract.DataStorages;
using Global.Saves;
using UnityEngine;

namespace Features.GamePlay.Save.Runtime
{
    public class GameSaver : IGameSaver
    {
        public GameSaver(IScore score, IDataStorage dataStorage)
        {
            _score = score;
            _dataStorage = dataStorage;
        }

        private const float _temporarySaveRate = 5f;

        private readonly IScore _score;
        private readonly IDataStorage _dataStorage;

        private float _lastSaveTime = 0f;

        public void SaveTemporary(List<BoardStateBlock> boardState)
        {
            if (_lastSaveTime + _temporarySaveRate > Time.time)
                return;

            _lastSaveTime = Time.time;
            
            var data = new GameSave(boardState, _score.Max.Value);
            _dataStorage.Save(data).Forget();
        }

        public void ForceSave(List<BoardStateBlock> boardState)
        {
            var data = new GameSave(boardState, _score.Max.Value);
            _dataStorage.Save(data).Forget();
        }
    }
}