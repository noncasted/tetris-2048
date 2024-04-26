using Internal.Scopes.Abstract.Containers;
using UnityEngine;

namespace Global.Configs.Abstract
{
    public abstract class ConfigSource : ScriptableObject
    {
        public abstract void CreateInstance(IServiceCollection services);
    }
}