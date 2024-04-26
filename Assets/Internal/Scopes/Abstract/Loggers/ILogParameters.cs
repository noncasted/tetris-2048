using System.Collections.Generic;

namespace Internal.Scopes.Abstract.Loggers
{
    public interface ILogParameters
    {
        ILogBodyParameters BodyParameters { get; }
        IReadOnlyList<ILogHeader> Headers { get; }
    }
}