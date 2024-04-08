using UnityEngine;

namespace _Project.Scripts.Gameplay.GameGrid
{
    public static class GridExtension
    {
        public static bool CanMoveTo(this GridModel grid, Vector2Int pos1, Vector2Int direction)
        {
            return InBounds(grid, pos1) 
                   && InBounds(grid, pos1 + direction) 
                   && !(IsDirectionUp(direction) && grid.IsEmptyAt(pos1 + direction));
        }
        
        public static void SwapValues(this GridModel grid, Vector2Int pos1, Vector2Int pos2)
        {
            if (!InBounds(grid, pos1) || !InBounds(grid, pos2))
            {
                Debug.LogError("Out of bounds");
                return;
            }

            int val1 = grid.Get(pos1.x, pos1.y);
            int val2 = grid.Get(pos2.x, pos2.y);
            
            grid.Set(pos1.x,pos1.y, val2);
            grid.Set(pos2.x,pos2.y, val1);
        }
        
        private static bool InBounds(this GridModel grid, Vector2Int pos)
        {
            return pos.x < grid.SizeX && pos.x >= 0 && pos.y < grid.SizeY && pos.y >= 0;
        }
        
        private static bool IsDirectionUp(Vector2Int direction)
        {
            return direction == new Vector2Int(0, 1);
        }
    }
}