using System;
using System.Threading;
using Internal.Scopes.Abstract.Lifetimes;

namespace Internal.Scopes.Runtime.Lifetimes
{
    public class TerminatedLifetime : ILifetime
    {
        public CancellationToken Token => CancellationToken.None;
        public bool IsTerminated => true;
        
        public void ListenTerminate(Action callback)
        {
        }

        public void RemoveTerminationListener(Action callback)
        {
        }

        public ILifetime CreateChild()
        {
            return new TerminatedLifetime();
        }

        public void Terminate()
        {
        }
    }
}