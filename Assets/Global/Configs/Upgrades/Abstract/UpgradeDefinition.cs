using UnityEngine;

namespace Global.Configs.Upgrades.Abstract
{
    public abstract class UpgradeDefinition : ScriptableObject, IUpgradeDefinition
    {
        [SerializeField] private string _key;

        public string Key => _key;
    }
}