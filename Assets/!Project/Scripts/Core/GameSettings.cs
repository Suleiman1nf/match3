using UnityEngine;

namespace _Project.Scripts.Core
{
    [CreateAssetMenu(menuName = "Game/Create GameSettings", fileName = "GameSettings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        public const int GridSizeX = 6;
        public const int GridSizeY = 10;
    }
}