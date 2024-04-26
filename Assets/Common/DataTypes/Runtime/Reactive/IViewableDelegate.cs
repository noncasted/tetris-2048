using System;
using Internal.Scopes.Abstract.Lifetimes;

namespace Common.DataTypes.Runtime.Reactive
{
    public interface IViewableDelegate
    {
        void Listen(IReadOnlyLifetime lifetime, Action listener);
    }
    
    public interface IViewableDelegate<T>
    {
        void Listen(IReadOnlyLifetime lifetime, Action<T> listener);
    }
    
    public interface IViewableDelegate<T1, T2>
    {
        void Listen(IReadOnlyLifetime lifetime, Action<T1, T2> listener);
    }
    
    public interface IViewableDelegate<T1, T2, T3>
    {
        void Listen(IReadOnlyLifetime lifetime, Action<T1, T2, T3> listener);
    }
}