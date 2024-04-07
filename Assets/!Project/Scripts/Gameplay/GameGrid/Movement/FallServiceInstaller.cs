using Zenject;

namespace _Project.Scripts.Gameplay.GameGrid.Movement
{
    public class FallServiceInstaller : Installer<FallServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<FallService>().FromComponentInHierarchy().AsSingle();
        }
    }
}