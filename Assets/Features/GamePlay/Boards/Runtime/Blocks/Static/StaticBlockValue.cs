﻿using Common.DataTypes.Runtime.Reactive;
using Features.GamePlay.Boards.Abstract.Blocks;
using Features.GamePlay.Boards.Abstract.Boards;
using Internal.Scopes.Abstract.Lifetimes;
using Shapes;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Features.GamePlay.Boards.Runtime.Blocks.Static
{
    public class StaticBlockValue : MonoBehaviour, IBoardBlockValue
    {
        [SerializeField] private ShapeRenderer _shape;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private BlockColors _colors;

        [SerializeField] [ReadOnly] private int _value;

        private readonly ViewableDelegate<int, BlockUpgradeData> _upgradeStarted = new();

        private bool _wasUpgradedCurrentMove;

        public bool IsUpgradeStarted => _wasUpgradedCurrentMove;
        public int Number => _value;
        public IViewableDelegate<int, BlockUpgradeData> UpgradeStarted => _upgradeStarted;

        public void Construct(
            IReadOnlyLifetime lifetime,
            IBoardLifecycle lifecycle,
            int value)
        {
            _value = value;
            _text.text = value.ToString();
            _shape.Color = _colors.GetColor(value);
            
            lifecycle.MoveStarted.Listen(lifetime, OnMoveStarted);
            lifecycle.BoardClear.Listen(lifetime, OnBoardClear);
            lifetime.ListenTerminate(OnBoardClear);
        }

        public void StartUpgrade(BlockUpgradeData data)
        {
            _wasUpgradedCurrentMove = true;
            _value *= 2;
            _upgradeStarted?.Invoke(_value, data);
        }

        public void CompleteUpgrade()
        {
            _shape.Color = _colors.GetColor(_value);
            _text.text = _value.ToString();
        }

        public bool CanBeMergedWith(int value)
        {
            if (_wasUpgradedCurrentMove == true)
                return false;

            return value == Number;
        }

        private void OnMoveStarted()
        {
            _wasUpgradedCurrentMove = false;
        }

        private void OnBoardClear()
        {
            _wasUpgradedCurrentMove = false;
        }
    }
}