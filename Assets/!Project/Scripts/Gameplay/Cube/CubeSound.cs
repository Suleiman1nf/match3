using _Project.Scripts.Core.Audio;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Cube
{
    public class CubeSound : MonoBehaviour
    {
        private AudioService _audioService;

        [Inject]
        private void Construct(AudioService audioService)
        {
            _audioService = audioService;
        }
        
        public void OnPopEvent()
        {
            _audioService.PlayEffect(_audioService.AudioData.Pop, 0.2f);
        }
    }
}