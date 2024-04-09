using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace _Project.Scripts.Core.Audio
{
    [CreateAssetMenu(menuName = "Game/Create GameAudioData", fileName = "GameAudioData", order = 0)]
    public class GameAudioData : ScriptableObject
    {
        [field: SerializeField] public AudioMixer AudioMixer { get; private set; }
        [field: SerializeField] public List<AudioClip> MusicContainer { get; private set; }

        [field: SerializeField] public AudioClip Move { get; private set; }

        [field: SerializeField] public AudioClip Pop { get; private set; }
    }
}