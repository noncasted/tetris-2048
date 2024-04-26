using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Global.System.Updaters.Delays;

namespace Global.Backend.Transactions
{
    public class TransactionRunner : ITransactionRunner
    {
        public TransactionRunner(IDelayRunner delayRunner)
        {
            _delayRunner = delayRunner;
        }

        private readonly IDelayRunner _delayRunner;
        
        public UniTask<T> Run<T>(
            Func<bool, CancellationToken, UniTask<T>> action,
            float timeout = 15,
            float retryDelay = 0.5f) where T : class
        {
            return new ResultTransactionalOperation<T>(_delayRunner, action, timeout, retryDelay).Run();
        }

        public UniTask Run(
            Func<bool, CancellationToken, UniTask> action,
            float timeout = 15f,
            float retryDelay = 0.5f)
        {
            return new EmptyTransactionalOperation(_delayRunner, action, timeout, retryDelay).Run();
        }
    }
}