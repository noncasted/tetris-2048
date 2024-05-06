using Common.DataTypes.Runtime.Reactive;
using Cysharp.Threading.Tasks;
using Features.GamePlay.Loop.Scores.Abstract;
using Global.Publisher.Abstract.DataStorages;
using Global.Saves;
using Internal.Scopes.Abstract.Instances.Services;

namespace Features.GamePlay.Loop.Scores.Runtime
{
    public class Score : IScopeAwakeAsyncListener, IScore
    {
        public Score(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        private readonly ViewableProperty<int> _current = new(0);
        private readonly ViewableProperty<int> _max = new(0);
        
        private readonly IDataStorage _dataStorage;
        
        public IViewableProperty<int> Current => _current;
        public IViewableProperty<int> Max => _max;
        
        public async UniTask OnAwakeAsync()
        {
            var save = await _dataStorage.GetEntry<GameSave>();
            _current.Set(save.GetScore());
            _max.Set(save.MaxScore);
        }
        
        public void SetCurrent(int current)
        {
            _current.Set(current);

            if (current > _max.Value)
                _max.Set(current);
        }
    }
}