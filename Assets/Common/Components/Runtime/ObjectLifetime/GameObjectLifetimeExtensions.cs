using Common.Components.Abstract;
using Internal.Scopes.Abstract.Lifetimes;
using UnityEngine;

namespace Common.Components.Runtime.ObjectLifetime
{
    public static class GameObjectLifetimeExtensions
    {
        public static IReadOnlyLifetime GetObjectLifetime(this MonoBehaviour monoBehaviour)
        {
            if (monoBehaviour.TryGetComponent(out IGameObjectLifetime lifetimeSource) == false)
                lifetimeSource = monoBehaviour.gameObject.AddComponent<GameObjectLifetime>();
            
            return lifetimeSource.GetValidLifetime();
        }
    }
}