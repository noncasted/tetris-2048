using Cysharp.Threading.Tasks;
using Features.Loop.Setup;
using Global.Common.Mocks.Runtime;
using UnityEngine;

namespace Features.Loop.Mock
{
    public class GameMock : MockBase
    {
        [SerializeField] private GameScopeConfig _gameScopeConfig;

        public override async UniTaskVoid Process()
        {
            await LoadChildScope(_gameScopeConfig);
        }
    }
}