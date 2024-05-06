using System;
using Common.DataTypes.Runtime.Reactive;
using Features.GamePlay.Boards.Abstract.Blocks;
using Features.GamePlay.Boards.Abstract.Boards;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.GamePlay.Boards.Runtime.Boards
{
    [DisallowMultipleComponent]
    public class BoardTile : MonoBehaviour, IBoardTile
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Vector2Int _position;
        [SerializeField] private bool _isLocked;
        [SerializeField] [ReadOnly] private bool _isTaken;

        private IBoardBlock _block;

        private readonly ViewableDelegate _blockEntered = new();

        public Vector2Int BoardPosition => _position;
        public Vector2 Position => _transform.position;
        public IBoardBlock Block => _block;

        public bool IsLocked => _isLocked;
        public bool IsTaken => _isTaken;

        public IViewableDelegate BlockEntered => _blockEntered;

        public void SetBlock(IBoardBlock block)
        {
            if (_isTaken == true)
            {
                if (_block == null)
                    throw new Exception($"Tile {BoardPosition} is already taken by moving block");

                throw new Exception($"Tile {BoardPosition} is already taken by static block");
            }
            
            _block = block;
            _isTaken = true;
            _blockEntered.Invoke();
        }

        public void RemoveBlock(IBoardBlock block)
        {
            if (_block != block)
                throw new Exception();

            _isTaken = false;
            _block = null;
        }

        public void Take()
        {
            _isTaken = true;
            _blockEntered.Invoke();
        }

        public void Free()
        {
            _isTaken = false;
        }
    }
}