using UnityEngine;

namespace _Project.Scripts.Core
{
    [CreateAssetMenu(menuName = "Game/Create LevelData", fileName = "Level", order = 0)]
    public class LevelData : ScriptableObject
    {
        [field: SerializeField] public TextAsset GridFile { get; private set; }
        [field: SerializeField] public GameObject Background { get; private set; }
    }
}