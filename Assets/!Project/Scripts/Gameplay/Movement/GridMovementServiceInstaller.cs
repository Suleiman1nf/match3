using Zenject;

namespace _Project.Scripts.Gameplay.Movement
{
    public class GridMovementServiceInstaller : Installer<GridMovementServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<GridMovementService>().FromComponentInHierarchy().AsSingle();
        }
    }
}