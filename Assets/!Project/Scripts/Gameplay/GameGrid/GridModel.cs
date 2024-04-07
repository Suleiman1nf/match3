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

        public int Get(int x, int y)
        {
            return _grid[x, y];
        }
    }
}