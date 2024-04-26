using Cysharp.Threading.Tasks;
using Global.Common.Mocks.Runtime;
using UnityEngine;

namespace Global.UI.Design.Test
{
    public class UIDesignMock : MockBase
    {
        [SerializeField] private Camera _camera;
        
        public override async UniTaskVoid Process()
        {
            var result = await BootstrapGlobal();
        }
    }
}