using System;
using Internal.Scopes.Abstract.Lifetimes;

namespace Global.System.Updaters.Progressions
{
    public interface IProgressionHandle
    {
        void Start(IReadOnlyLifetime lifetime, float time, Action<float> callback);
    }
}