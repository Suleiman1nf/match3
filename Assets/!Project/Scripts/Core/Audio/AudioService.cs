using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Core.Audio
{
    public class AudioService : MonoBehaviour
    {
        private const string MasterVolumeParam = "MasterVolume";
        private const string MusicVolumeParam = "MusicVolume";
        private const string SoundVolumeParam = "SoundVolume";
        private const float MinVolume = 0.0001f;

        [field: SerializeField] public GameAudioData Data { get; private set; }
        [SerializeField] private AudioSource _musicAudio;
        [SerializeField] private AudioSource _effectsAudio;

        public event Action<bool> OnMuteMaster;
        public event Action<bool> OnMuteMusic;
        public event Action<bool> OnMuteSound;

        public bool IsSoundMuted => _audioSave.soundMuted;
        public bool IsMusicMuted => _audioSave.musicMuted;
        public bool IsMasterMuted => _audioSave.masterMuted;

        private AudioSave _audioSave;
        
        public void Init(AudioSave audioSave)
        {
            _audioSave = audioSave;
            MuteMusic(_audioSave.musicMuted);
            MuteSoundEffects(_audioSave.soundMuted);
            MuteMaster(_audioSave.masterMuted);
        }
        
        public void PlayMusic(int musicId)
        {
            _musicAudio.clip = Data.MusicContainer[musicId];
            _musicAudio.Play();
        }

        public void PlayRandomMusic()
        {
            PlayMusic(Random.Range(0, Data.MusicContainer.Count));
        }

        public void PlayEffect(AudioClip clip, float volume = 1)
        {
            _effectsAudio.PlayOneShot(clip, volume);
        }

        public void MuteMaster(bool state)
        {
            ChangeMasterVolume(state ? 0 : 1f);
            _audioSave.masterMuted = state;
            OnMuteMaster?.Invoke(state);
        }

        public void MuteSoundEffects(bool state)
        {
            ChangeSoundVolume(state ? 0 : 1f);
            _audioSave.soundMuted = state;
            OnMuteSound?.Invoke(state);
        }

        public void MuteMusic(bool state)
        {
            ChangeMusicVolume(state ? 0 : 1f);
            _audioSave.musicMuted = state;
            OnMuteMusic?.Invoke(state);
        }

        public void StopMusic()
        {
            _musicAudio.Stop();
        }

        private void ChangeSoundVolume(float volume)
        {
            ChangeVolume(SoundVolumeParam, volume);
        }
        
        private void ChangeMusicVolume(float volume)
        {
            ChangeVolume(MusicVolumeParam, volume);
        }
        
        private void ChangeMasterVolume(float volume)
        {
            ChangeVolume(MasterVolumeParam, volume);
        }

        private void ChangeVolume(string param, float volume)
        {
            volume = Mathf.Clamp(volume, MinVolume, 1f);
            volume = Mathf.Log10(volume) * 20f;
            Data.AudioMixer.SetFloat(param, volume);
        }
    }
}
