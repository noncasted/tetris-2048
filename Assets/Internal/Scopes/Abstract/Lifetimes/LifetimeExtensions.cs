using UnityEngine.Events;

namespace Internal.Scopes.Abstract.Lifetimes
{
    public static class LifetimeExtensions
    {
        public static void Listen<T>(this UnityEvent<T> source, IReadOnlyLifetime lifetime, UnityAction<T> listener)
        {
            source.AddListener(listener);
            
            lifetime.ListenTerminate(() => source.RemoveListener(listener));
        }
    }
}