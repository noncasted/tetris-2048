using System;
using System.Threading;

namespace Internal.Scopes.Abstract.Lifetimes
{
    public interface IReadOnlyLifetime
    {
        CancellationToken Token { get; }
        bool IsTerminated { get; }

        void ListenTerminate(Action callback);
        void RemoveTerminationListener(Action callback);

        ILifetime CreateChild();
    }
}