using Internal.Scopes.Abstract.Loggers;
using Internal.Scopes.Abstract.Options;
using Internal.Setup.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Internal.Services.Loggers.Runtime
{
    [InlineEditor]
    public class LoggerFactory : ScriptableObject, IInternalServiceFactory
    {
        public void Create(IOptions options, IContainerBuilder services)
        {
            services.Register<Logger>(Lifetime.Singleton)
                .As<IGlobalLogger>();
        }
    }
}