using System.Collections.Generic;
using Internal.Scopes.Abstract.Options;

namespace Internal.Setup.Abstract
{
    public interface IInternalScopeConfig
    {
        InternalScope Scope { get; }
        IOptions Options { get; }
        IOptionsSetup OptionsSetup { get; }
        IReadOnlyList<IInternalServiceFactory> Services { get; }
    }
}