using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Gameplay.GameGrid.Behaviours
{
    public class FallService
    {

        public List<MoveData> ProcessFall(GridModel grid)
        {
            List<MoveData> moves = new List<MoveData>();
            for (int y = 1; y < grid.SizeY; y++)
            {
                for (int x = 0; x < grid.SizeX; x++)
                {
                    if (grid.IsEmptyAt(x, y))
                    {
                        continue;
                    }
                    Vector2Int position = new Vector2Int(x, y);
                    Vector2Int fallPosition = FallPosition(grid, position);
                    if (position != fallPosition)
                    {
                        int value = grid.Get(position);
                        grid.SetEmpty(position);
                        grid.Set(fallPosition, value);
                        moves.Add(new MoveData(position, fallPosition));
                    }
                }
            }

            return moves;
        }

        private Vector2Int FallPosition(GridModel grid, Vector2Int blockPosition)
        {
            int x = blockPosition.x;
            int y = blockPosition.y-1;

            while (grid.IsEmptyAt(x, y))
            {
                y--;
                if (y < 0)
                {
                    break;
                }
            }

            return new Vector2Int(x, y+1);
        }
    }
}