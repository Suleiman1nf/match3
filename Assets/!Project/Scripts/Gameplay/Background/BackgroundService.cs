using UnityEngine;

namespace _Project.Scripts.Gameplay.Background
{
    public class BackgroundService : MonoBehaviour
    {
        [SerializeField] private Transform _backgroundContainer;

        public void SetBackground(GameObject backgroundPrefab)
        {
            GameObject go = Instantiate(backgroundPrefab, _backgroundContainer);
        }
    }
}