namespace _Project.Scripts.Gameplay.GameGrid
{
    public class GridModel
    {
        private readonly int _rows;
        private readonly int _columns;
        private readonly int[,] _grid;

        public GridModel(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
            _grid = new int[_rows, _columns];
        }

        public void Set(int x, int y, int value)
        {
            _grid[x, y] = value;
        }

        public int Get(int x, int y)
        {
            return _grid[x, y];
        }
    }
}