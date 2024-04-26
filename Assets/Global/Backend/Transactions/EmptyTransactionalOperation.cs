using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Global.System.Updaters.Delays;

namespace Global.Backend.Transactions
{
    public class EmptyTransactionalOperation : ResultTransactionalOperation<object>
    {
        public EmptyTransactionalOperation(
            IDelayRunner delayRunner,
            Func<bool, CancellationToken, UniTask> action,
            float timeout = 5f, float retryDelay = 0.5f) : base(
            delayRunner,
            async (isRetry, cancellation) =>
            {
                await action.Invoke(isRetry, cancellation);
                return new object();
            },
            timeout, retryDelay)
        {
        }
    }
}