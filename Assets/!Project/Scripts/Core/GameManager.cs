using _Project.Scripts.Core.Audio;
using _Project.Scripts.Core.Save;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core
{
    public class GameManager : MonoBehaviour
    {
        private SaveService _saveService;
        private AudioService _audioService;

        [Inject]
        public void Construct(SaveService saveService, AudioService audioService)
        {
            _saveService = saveService;
            _audioService = audioService;
        }

        public void Start()
        {
            _saveService.Load();
            _audioService.Init(_saveService.GameSave.AudioSave);
        }
    }
}
