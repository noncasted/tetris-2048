using Global.Setup.Scope;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Setup.Abstract;
using Internal.Setup.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Common.Mocks.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = "GlobalMockConfig", menuName = "Common/GlobalMockConfig")]
    public class GlobalMockConfig : ScriptableObject
    {
        [SerializeField] [Indent] private InternalScopeConfig _internal;
        [SerializeField] [Indent] private GlobalScopeConfig _global;

        public IInternalScopeConfig Internal => _internal;
        public IServiceScopeConfig Global => _global;
    }
}