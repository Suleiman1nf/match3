using UnityEngine;

namespace _Project.Scripts.Gameplay.GameGrid
{
    public class GridModel
    {
        private readonly int[,] _grid;

        public GridModel(int[,] arr)
        {
            _grid = arr;
        }

        public int SizeX => _grid.GetLength(0);
        public int SizeY => _grid.GetLength(1);

        public void Set(int x, int y, int value)
        {
            _grid[x, y] = value;
        }

        public void Set(Vector2Int position, int value)
        {
            Set(position.x, position.y, value);
        }

        public void SetEmpty(Vector2Int position)
        {
            Set(position, 0);
        }

        public int Get(int x, int y)
        {
            return _grid[x, y];
        }

        public int Get(Vector2Int pos)
        {
            return Get(pos.x, pos.y);
        }

        public bool IsEmptyAt(int x, int y)
        {
            return _grid[x, y] <= 0;
        }
        
        public bool IsEmptyAt(Vector2Int pos)
        {
            return IsEmptyAt(pos.x, pos.y);
        }

        public GridModel Clone()
        {
            GridModel gridModel = new GridModel(new int[SizeX, SizeY]);
            for (int i = 0; i < gridModel.SizeX; i++)
            {
                for (int j = 0; j < gridModel.SizeY; j++)
                {
                    gridModel.Set(i,j,_grid[i,j]);
                }
            }

            return gridModel;
        }

        public bool IsEmpty()
        {
            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                {
                    if (!IsEmptyAt(i, j))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}