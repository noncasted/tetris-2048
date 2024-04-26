using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Global.System.Updaters.Abstract;

namespace Global.System.Updaters.Delays
{
    public class DelayRunner : IDelayRunner
    {
        public DelayRunner(IUpdater updater)
        {
            _updater = updater;
        }
        
        private readonly IUpdater _updater;

        public UniTask RunDelay(float time)
        {
            var delay = new Delay(_updater, time, null, null);
            return delay.Run();
        }

        public UniTask RunDelay(float time, CancellationToken cancellation)
        {
            var delay = new Delay(_updater, time, null, cancellation);
            return delay.Run();
        }

        public UniTask RunDelay(float time, Action callback, CancellationToken cancellation)
        {
            var delay = new Delay(_updater, time, callback, cancellation);
            return delay.Run();
        }
    }
}