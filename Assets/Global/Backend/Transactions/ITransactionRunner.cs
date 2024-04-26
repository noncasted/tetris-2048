using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Global.Backend.Transactions
{
    public interface ITransactionRunner
    {
        public UniTask<T> Run<T>(
            Func<bool, CancellationToken, UniTask<T>> action,
            float timeout = 15f,
            float retryDelay = 0.5f) where T : class;

        public UniTask Run(
            Func<bool, CancellationToken, UniTask> action,
            float timeout = 15f,
            float retryDelay = 0.5f);
    }
}