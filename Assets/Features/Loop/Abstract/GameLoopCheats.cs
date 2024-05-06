using Common.DataTypes.Runtime.Reactive;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Loop.Abstract
{
    [InlineEditor]
    public class GameLoopCheats : ScriptableObject
    {
        private readonly ViewableDelegate _endGame = new();

        public IViewableDelegate EndGame => _endGame;
        
        [Button]
        private void EndGameInstant()
        {
            _endGame.Invoke();
        }
    }
}