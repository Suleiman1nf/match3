using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.GameGrid.Movement
{
    public class FallService : MonoBehaviour
    {
        private GridMovementService _gridMovementService;
        
        private GridModel _gridModel;
        
        [Inject]
        private void Construct(GridMovementService gridMovementService)
        {
            _gridMovementService = gridMovementService;
        }
        
        public void Init(GridModel gridModel)
        {
            _gridModel = gridModel;
        }

        public List<MoveData> ProcessFall()
        {
            List<MoveData> moves = new List<MoveData>();
            for (int y = 1; y < _gridModel.SizeY; y++)
            {
                for (int x = 0; x < _gridModel.SizeX; x++)
                {
                    Vector2Int position = new Vector2Int(x, y);
                    Vector2Int fallPosition = FallPosition(position);
                    if (position != fallPosition)
                    {
                        _gridMovementService.SwapValues(position, fallPosition);
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

            while (_gridModel.IsEmptyAt(x, y))
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