using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Global.System.Updaters.Delays
{
    public interface IDelayRunner
    {
        UniTask RunDelay(float time);
        UniTask RunDelay(float time, CancellationToken cancellation);
        UniTask RunDelay(float time, Action callback, CancellationToken cancellation);
    }
}