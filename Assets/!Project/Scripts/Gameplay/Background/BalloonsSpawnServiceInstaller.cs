using Zenject;

namespace _Project.Scripts.Gameplay.Background
{
    public class BalloonsSpawnServiceInstaller : Installer<BalloonsSpawnServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<BalloonsSpawnService>().FromComponentInHierarchy().AsSingle();
        }
    }
}