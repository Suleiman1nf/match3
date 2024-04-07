using Zenject;

namespace _Project.Scripts.Gameplay.Movement
{
    public class FallServiceInstaller : Installer<FallServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<FallService>().FromComponentInHierarchy().AsSingle();
        }
    }
}