namespace _Project.Scripts.Core.Audio
{
    public class MusicMasterButton : AbstractMuteButton
    {
        protected override bool IsMuted { get => AudioService.IsMasterMuted; }
        protected override void Mute(bool state)
        {
            AudioService.MuteMaster(state);
        }

        protected override void AddListener()
        {
            AudioService.OnMuteMaster += OnMute;
        }

        protected override void RemoveListener()
        {
            if (AudioService != null)
            {
                AudioService.OnMuteMaster -= OnMute;
            }
        }
    }
}