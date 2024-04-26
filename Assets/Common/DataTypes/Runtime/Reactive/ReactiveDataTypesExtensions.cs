using System;
using Internal.Scopes.Abstract.Lifetimes;

namespace Common.DataTypes.Runtime.Reactive
{
    public static class ReactiveDataTypesExtensions
    {
        public static void View<T>(this IViewableProperty<T> property, IReadOnlyLifetime lifetime, Action listener)
        {
            property.View(lifetime, (_, _) => listener?.Invoke());
        }
        
        public static void View<T>(this IViewableProperty<T> property, IReadOnlyLifetime lifetime, Action<T> listener)
        { 
            property.View(lifetime, (_, value) => listener?.Invoke(value));
        }
    }
}