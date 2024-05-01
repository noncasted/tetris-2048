using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GamePlay.Boards.Abstract.Boards;
using GamePlay.Save.Abstract;
using Global.Publisher.Abstract.DataStorages;
using Global.Saves;
using Internal.Scopes.Abstract.Lifetimes;
using Loop.Abstract;
using Tutorial.Runtime.Steps;
using Tutorial.Runtime.Steps.CombineWithFall;
using Tutorial.Runtime.Steps.EndConditions;
using Tutorial.Runtime.Steps.MoveAndCombine;
using Tutorial.Runtime.Steps.SpeedModifications;
using UnityEngine;

namespace Tutorial.Runtime
{
    public class TutorialState : ILoopState
    {
        public TutorialState(
            IBoard board,
            IGameState state,
            IDataStorage dataStorage,
            TutorialStep_MoveAndCombine step1,
            TutorialStep_CombineWithFall step2,
            TutorialStep_SpeedModifications step3,
            TutorialStep_EndConditions step4)
        {
            _board = board;
            _state = state;
            _dataStorage = dataStorage;

            _step1 = step1;
            _step2 = step2;
            _step3 = step3;
            _step4 = step4;
        }

        private readonly IBoard _board;
        private readonly IGameState _state;
        private readonly IDataStorage _dataStorage;
        private readonly TutorialStep_MoveAndCombine _step1;
        private readonly TutorialStep_CombineWithFall _step2;
        private readonly TutorialStep_SpeedModifications _step3;
        private readonly TutorialStep_EndConditions _step4;

        public async UniTask<GameStateType> Enter(IReadOnlyLifetime stateLifetime)
        {
            _state.Set(GameStateType.Tutorial);
            _board.Fill(stateLifetime, CreateSave());

            await HandleStep(_step1);
            await HandleStep(_step2);
            await HandleStep(_step3);
            await HandleStep(_step4);

            await _dataStorage.Save(new TutorialSave()
            {
                IsTutorialPassed = true
            });
            
            return GameStateType.Game;

            async UniTask HandleStep(ITutorialStep step)
            {
                var stepLifetime = stateLifetime.CreateChild();
                await step.Handle(stepLifetime);
                stepLifetime.Terminate();
            }
        }

        private GameSave CreateSave()
        {
            var boardTiles = new List<BoardStateBlock>()
            {
                new(new Vector2Int(0, 3), 2),
                new(new Vector2Int(3, 3), 2),
            };

            return new GameSave(boardTiles, 0);
        }
    }
}