using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Core.Audio
{
    public abstract class AbstractMuteButton : MonoBehaviour
    {
        [SerializeField] private Button _muteButton;
        [SerializeField] private Button _unmuteButton;
        
        protected AudioService AudioService { get; private set; }
        protected abstract bool IsMuted { get; }

        [Inject]
        public void Construct(AudioService audioService)
        {
            AudioService = audioService;
        }

        private void Start()
        {
            _muteButton.onClick.AddListener(Unmute);
            _unmuteButton.onClick.AddListener(Mute);
            AddListener();
            OnMute(IsMuted);
        }

        private void OnDestroy()
        {
            _muteButton.onClick.RemoveListener(Unmute);
            _unmuteButton.onClick.RemoveListener(Mute);
            RemoveListener();
        }

        public void Mute()
        {
            Mute(true);
        }

        public void Unmute()
        {
            Mute(false);
        }

        protected void OnMute(bool state)
        {
            _muteButton.gameObject.SetActive(state);
            _unmuteButton.gameObject.SetActive(!state);
        }

        protected abstract void Mute(bool state);

        protected abstract void AddListener();

        protected abstract void RemoveListener();
    }
}