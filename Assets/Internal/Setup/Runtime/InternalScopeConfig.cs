using System.Collections.Generic;
using Internal.Common;
using Internal.Scopes.Abstract.Options;
using Internal.Services.Loggers.Runtime;
using Internal.Services.Options.Runtime;
using Internal.Services.Scenes.Setup;
using Internal.Setup.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Internal.Setup.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = InternalRoutes.ConfigName, menuName = InternalRoutes.ConfigPath)]
    public class InternalScopeConfig : ScriptableObject, IInternalScopeConfig
    {
        [SerializeField] private ScenesFlowFactory _scenes;
        [SerializeField] private LoggerFactory _logger;
        [SerializeField] private Options _options;
        [SerializeField] private InternalScope _scope;

        public InternalScope Scope => _scope;
        public IOptions Options => _options;
        public IOptionsSetup OptionsSetup => _options;

        public IReadOnlyList<IInternalServiceFactory> Services => new IInternalServiceFactory[]
        {
            _scenes,
            _logger
        };
    }
}