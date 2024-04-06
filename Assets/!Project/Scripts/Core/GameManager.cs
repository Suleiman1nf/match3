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

        [Inject]
        public void Construct(SaveService saveService, AudioService audioService, BackgroundService backgroundService)
        {
            _saveService = saveService;
            _audioService = audioService;
            _backgroundService = backgroundService;
        }

        public void Start()
        {
            _saveService.Load();
            _audioService.Init(_saveService.GameSave.AudioSave);
            _backgroundService.SetBackground(_testBackground);
        }
    }
}
