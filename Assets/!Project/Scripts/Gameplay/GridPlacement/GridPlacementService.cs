using UnityEngine;

namespace _Project.Scripts.Gameplay.GridPlacement
{
    public class GridPlacementService : MonoBehaviour
    {
        [SerializeField] private float _offsetSides;
        [SerializeField] private float _offsetBottom;
        [SerializeField] private float _offsetTop;
        [SerializeField] private Grid _grid;

        private Camera _camera;
        private int _rows;
        private int _columns;

        public void Init(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
            _camera = Camera.main;
            SetToBottomLeftCorner();
            SetCellSize();
        }

        public void FillGrid(GameObject prefab)
        {
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    GameObject go = Instantiate(prefab);
                    go.transform.position = GetPosition(i, j);
                    go.transform.localScale = _grid.cellSize;
                }
            }
        }
        
        public Vector2 GetPosition(int x, int y)
        {
            return _grid.GetCellCenterWorld(new Vector3Int(x, y));
        }

        private void SetCellSize()
        {
            float unitsPerPixel = _camera.orthographicSize * 2 / Screen.height;
            float screenWidthInUnits = Screen.width * unitsPerPixel;
            float screenHeightInUnits = Screen.height * unitsPerPixel;
            float width = screenWidthInUnits - _offsetSides * 2;
            float height = screenHeightInUnits - _offsetBottom - _offsetTop;
            float cellHeight = height / _columns;
            float cellWidth = width / _rows;
            float cell = Mathf.Min(cellHeight, cellWidth);
            Vector2 cellSize = new Vector2(cell, cell);
            _grid.cellSize = cellSize;
        }

        private void SetToBottomLeftCorner()
        {
            Vector3 bottomLeftScreenPos = new Vector3(0, 0, _camera.nearClipPlane);
            Vector3 bottomLeftWorldPos = _camera.ScreenToWorldPoint(bottomLeftScreenPos);
            _grid.transform.position = bottomLeftWorldPos;
            _grid.transform.position += new Vector3(_offsetSides, _offsetBottom);
        }
    }
}