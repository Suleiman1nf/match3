using UnityEngine;

namespace _Project.Scripts.Gameplay.GameGrid.Movement
{
    public class MoveData
    {
        public Vector2Int Origin { get; private set; }

        public Vector2Int Destination {  get; private set;}

        public MoveData(Vector2Int origin, Vector2Int destination)
        {
            Origin = origin;
            Destination = destination;
        }
    }
}