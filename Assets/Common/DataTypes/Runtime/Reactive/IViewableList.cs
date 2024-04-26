using System;
using Internal.Scopes.Abstract.Lifetimes;

namespace Common.DataTypes.Runtime.Reactive
{
    public interface IViewableList<T>
    {
        void View(IReadOnlyLifetime lifetime, Action<IReadOnlyLifetime, T> listener);
    }
}