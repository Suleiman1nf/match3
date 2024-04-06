using Zenject;

namespace _Project.Scripts.Gameplay.Background
{
    public class BackgroundServiceInstaller : Installer<BackgroundServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<BackgroundService>().FromComponentInHierarchy().AsSingle();
        }
    }
}