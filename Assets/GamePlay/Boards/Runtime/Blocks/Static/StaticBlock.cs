using System;
using GamePlay.Boards.Abstract.Blocks;
using GamePlay.Boards.Abstract.Boards;
using GamePlay.Boards.Runtime.Blocks.Static.Actions;
using Global.System.Updaters.Progressions;
using Internal.Scopes.Abstract.Lifetimes;
using UnityEngine;

namespace GamePlay.Boards.Runtime.Blocks.Static
{
    [DisallowMultipleComponent]
    public class StaticBlock : MonoBehaviour, IBoardBlock, IStaticBlockInteractor
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private BlockConfig _config;
        [SerializeField] private StaticBlockValue _value;

        private IBoardTile _tile;
        private ILifetime _lifetime;

        private IProgressionHandle _progressionHandle;
        private IStaticBlockAction _action;

        public IBoardBlockValue Value => _value;
        public IBoardTile Tile => _tile;

        public void Construct(
            IReadOnlyLifetime lifetime,
            IProgressionFactory progressionFactory,
            IBoardTile tile,
            IBoardLifecycle lifecycle,
            int value)
        {
            _tile = tile;
            _transform.position = tile.Position;

            _lifetime = lifetime.CreateChild();
            _progressionHandle = progressionFactory.CreateHandle();

            _value.Construct(_lifetime, lifecycle, value);
        }

        public void SetTile(IBoardTile tile)
        {
            _tile = tile;
        }

        public void MoveTo(IBoardTile target)
        {
            _action?.Break();

            var moveOptions = new StaticBlockMoveOptions(_tile, target, this, _transform, _config);

            if (target.Block == null)
                _action = new StaticBlockMoveAction(_lifetime, _progressionHandle, this, moveOptions);
            else
                _action = new StaticBlockUpdateAction(_lifetime, _progressionHandle, this, moveOptions);

            _action.Start();
        }

        public void DestroyBlock()
        {
            if (_value.IsUpgradeStarted == true)
                throw new Exception();

            _lifetime.Terminate();
            Destroy(gameObject);
        }
    }
}