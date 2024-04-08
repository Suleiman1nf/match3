using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Core
{
    [CreateAssetMenu(menuName = "Game/Create GameSettings", fileName = "GameSettings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        [field: SerializeField] public float SideMoveDuration { get; private set; }
        [field: SerializeField] public Ease SideMoveEase { get; private set; }
        [field: SerializeField] public float FallDuration { get; private set; }
        [field: SerializeField] public Ease FallEase { get; private set; }
    }
}