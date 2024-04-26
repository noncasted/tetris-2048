using Cysharp.Threading.Tasks;
using Global.Common.Mocks.Runtime;
using Loop.Setup;
using UnityEngine;

namespace Loop.Mock
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