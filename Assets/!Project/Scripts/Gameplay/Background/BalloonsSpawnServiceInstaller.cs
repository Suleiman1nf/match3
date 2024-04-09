using Zenject;

namespace _Project.Scripts.Gameplay.Background
{
    public class BalloonsSpawnServiceInstaller : Installer<BalloonsSpawnService.Settings, BalloonsSpawnServiceInstaller>
    {
        private readonly BalloonsSpawnService.Settings _settings;

        public BalloonsSpawnServiceInstaller(BalloonsSpawnService.Settings settings)
        {
            _settings = settings;
        }

        public override void InstallBindings()
        {
            Container.Bind<BalloonsSpawnService>().FromNew().AsSingle().WithArguments(_settings);
        }
    }
}