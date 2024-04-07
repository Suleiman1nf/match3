using UnityEngine;

namespace _Project.Scripts.Gameplay.Cube
{
    public class CubeGridData : MonoBehaviour
    {
        private Vector2Int _gridPosition;
        
        public Vector2Int GetPosition()
        {
            return _gridPosition;
        }
        
        public void SetPosition(Vector2Int pos)
        {
            _gridPosition = pos;
        }        
    }
}