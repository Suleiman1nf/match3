using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Gameplay.GameGrid.Behaviours
{
    public class SwapService
    {
        public List<MoveData> Swap(GridModel grid, Vector2Int position, Vector2Int direction)
        {
            List<MoveData> moves = new List<MoveData>();
            Vector2Int origin = position;
            Vector2Int destination = origin + direction;
            if (!grid.IsEmptyAt(origin) 
                && grid.CanMoveTo(origin, direction))
            {
                grid.SwapValues(origin, destination);
                moves.Add(new MoveData(origin, destination));
                moves.Add(new MoveData(destination, origin));
            }

            return moves;
        }
    }
}