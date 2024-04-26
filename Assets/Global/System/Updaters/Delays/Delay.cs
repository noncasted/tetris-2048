using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Global.System.Updaters.Abstract;

namespace Global.System.Updaters.Delays
{
    public class Delay : IDelay, IUpdatable
    {
        public Delay(
            IUpdater updater,
            float delay,
            Action callback,
            CancellationToken? cancellation)
        {
            _updater = updater;
            _delay = delay;
            _callback = callback;
            _cancellation = cancellation;
            _completion = new UniTaskCompletionSource();
        }

        private readonly IUpdater _updater;
        private readonly float _delay;
        private readonly Action _callback;
        private readonly CancellationToken? _cancellation;
        private readonly UniTaskCompletionSource _completion;

        private float _timer;
        private bool _wasCanceled;
        
        public async UniTask Run()
        {
            _cancellation?.Register(OnCanceled);
            _updater.Add(this);
            
            await _completion.Task;

            _callback?.Invoke();
            _updater.Remove(this);
        }

        public void OnUpdate(float delta)
        {
            _timer += delta;
            
            if (_timer < _delay)
                return;

            _completion.TrySetResult();
        }

        private void OnCanceled()
        {
            if (_wasCanceled == true)
                return;

            _wasCanceled = true;
            _updater.Remove(this);
        }
    }
}