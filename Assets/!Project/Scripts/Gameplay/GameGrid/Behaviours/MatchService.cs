using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts.Gameplay.GameGrid.Behaviours
{
    public class MatchService
    {
        public void DestroyMatches(GridModel gridModel, List<Vector2Int> matches)
        {
            foreach (Vector2Int pos in matches)
            {
                gridModel.SetEmpty(pos);
            }
        }
        
        public List<Vector2Int> FindMatches(GridModel grid)
        {
            List<Vector2Int> matchedBlocks = new List<Vector2Int>();

            for (int y = 0; y < grid.SizeY; y++)
            {
                for (int x = 0; x < grid.SizeX - 2; x++)
                {
                    int currentValue = grid.Get(x, y);
                    if (!grid.IsEmptyAt(x,y) && grid.Get(x + 1, y) == currentValue && grid.Get(x + 2, y) == currentValue)
                    {
                        matchedBlocks.Add(new Vector2Int(x, y));
                        matchedBlocks.Add(new Vector2Int(x + 1, y));
                        matchedBlocks.Add(new Vector2Int(x + 2, y));
                    }
                }
            }

            for (int x = 0; x < grid.SizeX; x++)
            {
                for (int y = 0; y < grid.SizeY - 2; y++)
                {
                    int currentValue = grid.Get(x, y);
                    if (!grid.IsEmptyAt(x,y) && grid.Get(x, y + 1) == currentValue && grid.Get(x, y + 2) == currentValue)
                    {
                        matchedBlocks.Add(new Vector2Int(x, y));
                        matchedBlocks.Add(new Vector2Int(x, y + 1));
                        matchedBlocks.Add(new Vector2Int(x, y + 2));
                    }
                }
            }
            
            List<Vector2Int> connectedBlocks = new List<Vector2Int>();
            foreach (Vector2Int block in matchedBlocks)
            {
                FindConnectedBlocks(block.x, block.y, grid.Get(block.x, block.y), grid, ref connectedBlocks);
            }
            
            foreach (Vector2Int block in connectedBlocks)
            {
                if (!matchedBlocks.Contains(block))
                    matchedBlocks.Add(block);
            }

            return matchedBlocks.Distinct().ToList();
        }

        private void FindConnectedBlocks(int x, int y, int targetType, GridModel grid, ref List<Vector2Int> connectedBlocks)
        {
            if (x < 0 || x >= grid.SizeX || y < 0 || y >= grid.SizeY || grid.Get(x,y) != targetType || connectedBlocks.Contains(new Vector2Int(x, y)))
            {
                return;
            }

            connectedBlocks.Add(new Vector2Int(x, y));

            // Check neighboring blocks
            FindConnectedBlocks(x - 1, y, targetType, grid, ref connectedBlocks); // Left
            FindConnectedBlocks(x + 1, y, targetType, grid, ref connectedBlocks); // Right
            FindConnectedBlocks(x, y - 1, targetType, grid, ref connectedBlocks); // Down
            FindConnectedBlocks(x, y + 1, targetType, grid, ref connectedBlocks); // Up
        }
    }
}