using _Project.Scripts.Core.Audio;
using _Project.Scripts.Core.Save;
using _Project.Scripts.Gameplay.Background;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject _testBackground;
        
        private SaveService _saveService;
        private AudioService _audioService;
        private BackgroundService _backgroundService;
        private BalloonsSpawnService _balloonsSpawnService;

        [Inject]
        public void Construct(SaveService saveService, AudioService audioService, BackgroundService backgroundService, BalloonsSpawnService balloonsSpawnService)
        {
            _saveService = saveService;
            _audioService = audioService;
            _backgroundService = backgroundService;
            _balloonsSpawnService = balloonsSpawnService;
        }

        public void Start()
        {
            _saveService.Load();
            _audioService.Init(_saveService.GameSave.AudioSave);
            _backgroundService.SetBackground(_testBackground);
            _balloonsSpawnService.StartSpawning();
        }
    }
}
