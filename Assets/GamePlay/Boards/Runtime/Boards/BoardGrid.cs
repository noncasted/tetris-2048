using Sirenix.OdinInspector;
using UnityEngine;

namespace GamePlay.Boards.Runtime.Boards
{
    [DisallowMultipleComponent]
    public class BoardGrid : MonoBehaviour
    {
        [SerializeField] private float _spacing;
        [SerializeField] private BoardTile[] _tiles;

        [Button]
        private void Snap()
        {
            if (_tiles == null || _tiles.Length == 0)
                _tiles = FindObjectsByType<BoardTile>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            var xSize = 0;
            var ySize = 0;

            foreach (var tile in _tiles)
            {
                if (tile.BoardPosition.x > xSize)
                    xSize = tile.BoardPosition.x;

                if (tile.BoardPosition.y > ySize)
                    ySize = tile.BoardPosition.y;
            }

            var xOffset = (xSize + _spacing * xSize) / -2f;
            var yOffset = (ySize + _spacing * ySize) / -2f;

            foreach (var tile in _tiles)
            {
                var boardPosition = tile.BoardPosition;
                var x = xOffset + boardPosition.x + boardPosition.x * _spacing;
                var yPosition = ySize - boardPosition.y;
                var y = yOffset + yPosition + yPosition * _spacing;
                var position = new Vector3(x, y, 0f);
                tile.transform.localPosition = position;
            }
        }
    }
}