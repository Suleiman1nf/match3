namespace _Project.Scripts.Core.Audio
{
    public class MuteSoundButton : AbstractMuteButton
    {
        protected override bool IsMuted { get => AudioService.IsSoundMuted; }
        protected override void Mute(bool state)
        {
            AudioService.MuteSoundEffects(state);
        }

        protected override void AddListener()
        {
            AudioService.OnMuteSound += OnMute;
        }

        protected override void RemoveListener()
        {
            if (AudioService != null)
            {
                AudioService.OnMuteSound -= OnMute;
            }
        }
    }
}