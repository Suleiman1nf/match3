namespace _Project.Scripts.Core.Audio
{
    public class MusicMuteButton : AbstractMuteButton
    {
        protected override bool IsMuted { get => AudioService.IsMusicMuted; }
        protected override void Mute(bool state)
        {
            AudioService.MuteMusic(state);
        }

        protected override void AddListener()
        {
            AudioService.OnMuteMusic += OnMute;
        }

        protected override void RemoveListener()
        {
            if (AudioService)
            {
                AudioService.OnMuteMusic -= OnMute;
            }
        }
    }
}