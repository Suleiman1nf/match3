using UnityEngine;

namespace _Project.Scripts.Gameplay.GameGrid.Behaviours
{
    public class GridService
    {
        public GridModel GridModel { get; private set; }

        public void Init(GridModel gridModel)
        {
            GridModel = gridModel;
        }

        public bool CanMoveTo(Vector2Int pos1, Vector2Int direction)
        {
            return InBounds(pos1) 
                   && InBounds(pos1 + direction) 
                   && !(IsDirectionUp(direction) && GridModel.IsEmptyAt(pos1 + direction));
        }
        
        public void SwapValues(Vector2Int pos1, Vector2Int pos2)
        {
            if (!InBounds(pos1) || !InBounds(pos2))
            {
                Debug.LogError("Out of bounds");
                return;
            }

            int val1 = GridModel.Get(pos1.x, pos1.y);
            int val2 = GridModel.Get(pos2.x, pos2.y);
            
            GridModel.Set(pos1.x,pos1.y, val2);
            GridModel.Set(pos2.x,pos2.y, val1);
        }

        private bool InBounds(Vector2Int pos)
        {
            return pos.x < GridModel.SizeX && pos.x >= 0 && pos.y < GridModel.SizeY && pos.y >= 0;
        }
        
        private bool IsDirectionUp(Vector2Int direction)
        {
            return direction == new Vector2Int(0, 1);
        }
    }
}