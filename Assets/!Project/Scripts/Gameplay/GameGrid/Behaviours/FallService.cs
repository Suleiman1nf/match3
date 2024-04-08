using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Gameplay.GameGrid.Behaviours
{
    public class FallService
    {
        private readonly GridService _gridService;
        
        private GridModel GridModel => _gridService.GridModel;
        
        public FallService(GridService gridService)
        {
            _gridService = gridService;
        }

        public List<MoveData> ProcessFall()
        {
            List<MoveData> moves = new List<MoveData>();
            for (int y = 1; y < GridModel.SizeY; y++)
            {
                for (int x = 0; x < GridModel.SizeX; x++)
                {
                    if (GridModel.IsEmptyAt(x, y))
                    {
                        continue;
                    }
                    Vector2Int position = new Vector2Int(x, y);
                    Vector2Int fallPosition = FallPosition(position);
                    if (position != fallPosition)
                    {
                        _gridService.SwapValues(position, fallPosition);
                        moves.Add(new MoveData(position, fallPosition));
                    }
                }
            }

            return moves;
        }

        private Vector2Int FallPosition(Vector2Int blockPosition)
        {
            int x = blockPosition.x;
            int y = blockPosition.y-1;

            while (GridModel.IsEmptyAt(x, y))
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