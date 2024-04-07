using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.GameGrid.World
{
    public class WorldGridService
    {
        private Settings _settings;

        private Camera _camera;
        private int _sizeX;
        private int _sizeY;

        public WorldGridService(Settings settings)
        {
            _settings = settings;
        }

        public void Init(int sizeX, int sizeY)
        {
            _sizeX = sizeX;
            _sizeY = sizeY;
            _camera = Camera.main;
            SetToBottomLeftCorner();
            SetCellSize();
        }
        
        public Vector2 GetPosition(int x, int y)
        {
            return _settings.Grid.GetCellCenterWorld(new Vector3Int(x, y));
        }

        public Vector2 GetPosition(Vector2Int pos)
        {
            return GetPosition(pos.x, pos.y);
        }

        private void SetCellSize()
        {
            float unitsPerPixel = _camera.orthographicSize * 2 / Screen.height;
            float screenWidthInUnits = Screen.width * unitsPerPixel;
            float screenHeightInUnits = Screen.height * unitsPerPixel;
            float width = screenWidthInUnits - _settings.OffsetSides * 2;
            float height = screenHeightInUnits - _settings.OffsetBottom - _settings.OffsetTop;
            float cellHeight = height / _sizeY;
            float cellWidth = width / _sizeX;
            float cell = Mathf.Min(cellHeight, cellWidth);
            Vector2 cellSize = new Vector2(cell, cell);
            _settings.Grid.cellSize = cellSize;
        }

        private void SetToBottomLeftCorner()
        {
            Vector3 bottomLeftScreenPos = new Vector3(0, 0, _camera.nearClipPlane);
            Vector3 bottomLeftWorldPos = _camera.ScreenToWorldPoint(bottomLeftScreenPos);
            _settings.Grid.transform.position = bottomLeftWorldPos + new Vector3(_settings.OffsetSides, _settings.OffsetBottom);
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public float OffsetSides { get; private set; }
            [field: SerializeField] public float OffsetBottom { get; private set; }
            [field: SerializeField] public float OffsetTop { get; private set; }
            [field: SerializeField] public Grid Grid { get; private set; }
        }
    }
}