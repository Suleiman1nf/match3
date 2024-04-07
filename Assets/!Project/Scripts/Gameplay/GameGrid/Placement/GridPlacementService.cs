using UnityEngine;

namespace _Project.Scripts.Gameplay.GameGrid.Placement
{
    public class GridPlacementService : MonoBehaviour
    {
        [SerializeField] private float _offsetSides;
        [SerializeField] private float _offsetBottom;
        [SerializeField] private float _offsetTop;
        [SerializeField] private Grid _grid;

        private Camera _camera;
        private int _sizeX;
        private int _sizeY;

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
            return _grid.GetCellCenterWorld(new Vector3Int(x, y));
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
            float width = screenWidthInUnits - _offsetSides * 2;
            float height = screenHeightInUnits - _offsetBottom - _offsetTop;
            float cellHeight = height / _sizeY;
            float cellWidth = width / _sizeX;
            float cell = Mathf.Min(cellHeight, cellWidth);
            Vector2 cellSize = new Vector2(cell, cell);
            _grid.cellSize = cellSize;
        }

        private void SetToBottomLeftCorner()
        {
            Vector3 bottomLeftScreenPos = new Vector3(0, 0, _camera.nearClipPlane);
            Vector3 bottomLeftWorldPos = _camera.ScreenToWorldPoint(bottomLeftScreenPos);
            _grid.transform.position = bottomLeftWorldPos + new Vector3(_offsetSides, _offsetBottom);
        }
    }
}