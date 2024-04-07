using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Core.Audio
{
    public class AudioService
    {
        private const string MasterVolumeParam = "MasterVolume";
        private const string MusicVolumeParam = "MusicVolume";
        private const string SoundVolumeParam = "SoundVolume";
        private const float MinVolume = 0.0001f;
        public event Action<bool> OnMuteMaster;
        public event Action<bool> OnMuteMusic;
        public event Action<bool> OnMuteSound;
        
        private AudioSave _audioSave;
        private Settings _settings;
        
        public bool IsSoundMuted => _audioSave.soundMuted;
        public bool IsMusicMuted => _audioSave.musicMuted;
        public bool IsMasterMuted => _audioSave.masterMuted;
        public GameAudioData AudioData => _settings.Data;

        public AudioService(Settings settings)
        {
            _settings = settings;
        }

        public void Init(AudioSave audioSave)
        {
            _audioSave = audioSave;
            MuteMusic(_audioSave.musicMuted);
            MuteSoundEffects(_audioSave.soundMuted);
            MuteMaster(_audioSave.masterMuted);
        }
        
        public void PlayMusic(int musicId)
        {
            _settings.MusicAudio.clip = _settings.Data.MusicContainer[musicId];
            _settings.MusicAudio.Play();
        }

        public void PlayRandomMusic()
        {
            PlayMusic(Random.Range(0, _settings.Data.MusicContainer.Count));
        }

        public void PlayEffect(AudioClip clip, float volume = 1)
        {
            _settings.EffectsAudio.PlayOneShot(clip, volume);
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
            _settings.MusicAudio.Stop();
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
            _settings.Data.AudioMixer.SetFloat(param, volume);
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public GameAudioData Data { get; private set; }
            [field: SerializeField] public AudioSource MusicAudio { get; private set; }
            [field: SerializeField] public AudioSource EffectsAudio { get; private set; }
        }
    }
}
