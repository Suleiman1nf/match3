using Zenject;

namespace _Project.Scripts.Core.Audio
{
    public class AudioServiceInstaller : Installer<AudioService.Settings, AudioServiceInstaller>
    {
        private AudioService.Settings _settings;
        public AudioServiceInstaller(AudioService.Settings settings)
        {
            _settings = settings;
        }

        public override void InstallBindings()
        {
            Container.Bind<AudioService>().FromNew().AsSingle().WithArguments(_settings).NonLazy();
        }
    }
}