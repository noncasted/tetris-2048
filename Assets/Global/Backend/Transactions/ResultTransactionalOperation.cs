using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Global.System.Updaters.Delays;
using UnityEngine;

namespace Global.Backend.Transactions
{
    public class ResultTransactionalOperation<T> where T : class
    {
        public ResultTransactionalOperation(
            IDelayRunner delayRunner,
            Func<bool, CancellationToken, UniTask<T>> action,
            float timeout,
            float retryDelay)
        {
            _delayRunner = delayRunner;
            _action = action;
            _timeout = timeout;
            _retryDelay = retryDelay;
        }

        private readonly IDelayRunner _delayRunner;
        private readonly Func<bool, CancellationToken, UniTask<T>> _action;
        private readonly float _timeout;
        private readonly float _retryDelay;

        private float _timer;

        public async UniTask<T> Run()
        {
            var isSuccess = false;
            var isRetry = false;
            T result = null;

            while (isSuccess == false)
            {
                var cancellation = new CancellationTokenSource();

                if (isRetry == true)
                    Debug.Log("Start transaction retry");

                try
                {
                    _delayRunner.RunDelay(_timeout, OnTimeout, cancellation.Token).Forget();
                    result = await _action.Invoke(isRetry, cancellation.Token);
                    isSuccess = true;
                }
                catch (Exception exception)
                {
                    Debug.Log($"Exception in result transaction: {exception.Message}");

                    cancellation.Cancel();
                    cancellation.Dispose();
                    cancellation = null;

                    isSuccess = false;
                    isRetry = true;

                    await _delayRunner.RunDelay(_retryDelay);
                }

                if (isRetry == true)
                    Debug.Log("Transaction completed with retry");

                cancellation?.Cancel();
                cancellation?.Dispose();
            }

            return result;

            void OnTimeout()
            {
                isSuccess = false;
                isRetry = true;
            }
        }
    }
}