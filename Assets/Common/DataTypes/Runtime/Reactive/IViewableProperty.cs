using System;
using Internal.Scopes.Abstract.Lifetimes;

namespace Common.DataTypes.Runtime.Reactive
{
    public interface IViewableProperty<T>
    {
        T Value { get; }

        void View(IReadOnlyLifetime lifetime, Action<IReadOnlyLifetime, T> listener);
    }
}