using _Project.Scripts.Gameplay.GameGrid;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Movement
{
    public class GridMovementService : MonoBehaviour
    {
        private GridModel _gridModel;

        public void Init(GridModel gridModel)
        {
            _gridModel = gridModel;
        }

        public bool CanMoveTo(Vector2Int pos1, Vector2Int direction)
        {
            return InBounds(pos1) 
                   && InBounds(pos1 + direction) 
                   && !(IsDirectionUp(direction) && _gridModel.IsEmptyAt(pos1 + direction));
        }
        
        public void SwapValues(Vector2Int pos1, Vector2Int pos2)
        {
            if (!InBounds(pos1) || !InBounds(pos2))
            {
                Debug.LogError("Out of bounds");
                return;
            }

            int val1 = _gridModel.Get(pos1.x, pos1.y);
            int val2 = _gridModel.Get(pos2.x, pos2.y);
            
            _gridModel.Set(pos1.x,pos1.y, val2);
            _gridModel.Set(pos2.x,pos2.y, val1);
        }

        private bool InBounds(Vector2Int pos)
        {
            return pos.x < _gridModel.SizeX && pos.x >= 0 && pos.y < _gridModel.SizeY && pos.y > 0;
        }
        
        private bool IsDirectionUp(Vector2Int direction)
        {
            return direction == new Vector2Int(0, 1);
        }
    }
}