using _Project.Scripts.Gameplay.InputManagement;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Cube
{
    public class CubeController : MonoBehaviour, ISwipeable
    {
        [field: SerializeField] public CubeAnimation CubeAnimation { get; private set; }
        [field: SerializeField] public CubeGridData CubeGridData { get; private set; }
        [field: SerializeField] public CubeMovement CubeMovement { get; private set; }
        public bool CanSwipe { get; set; } = true;
    }
}