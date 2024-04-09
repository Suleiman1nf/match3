using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Background
{
    public class BackgroundImageService
    {
        private readonly Settings _settings;

        public BackgroundImageService(Settings settings)
        {
            _settings = settings;
        }

        public void SetBackground(GameObject backgroundPrefab)
        {
            GameObject.Instantiate(backgroundPrefab, _settings.BackgroundContainer);
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Transform BackgroundContainer { get; private set; }
        }
    }
}