using Zenject;

namespace _Project.Scripts.Gameplay.Background
{
    public class BackgroundImageServiceInstaller : Installer<BackgroundImageService.Settings, BackgroundImageServiceInstaller>
    {
        private BackgroundImageService.Settings _settings;

        public BackgroundImageServiceInstaller(BackgroundImageService.Settings settings)
        {
            _settings = settings;
        }

        public override void InstallBindings()
        {
            Container.Bind<BackgroundImageService>().FromNew().AsSingle().WithArguments(_settings);
        }
    }
}