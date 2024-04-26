using Common.DataTypes.Runtime.Reactive;

namespace Global.Configs.Upgrades.Abstract
{
    public class Upgrade<T> : IUpgradeInstance
    {
        public Upgrade(IUpgradeDefinition definition, T[] levels)
        {
            _definition = definition;
            _levels = levels;
        }

        private readonly IUpgradeDefinition _definition;
        private readonly ViewableProperty<T> _value = new();
        private readonly T[] _levels;

        private int _currentLevel;

        public IUpgradeDefinition Definition => _definition;
        public int CurrentLevel => _currentLevel;
        public bool IsMaxLevel => CurrentLevel == _levels.Length;

        public IViewableProperty<T> Value => _value;
        
        public void SetLevel(int level)
        {
            _currentLevel = level;
            _value.Set(_levels[level]);
        }
    }
}