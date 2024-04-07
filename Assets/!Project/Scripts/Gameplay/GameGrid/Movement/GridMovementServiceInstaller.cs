using Zenject;

namespace _Project.Scripts.Gameplay.GameGrid.Movement
{
    public class GridMovementServiceInstaller : Installer<GridMovementServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<GridMovementService>().FromComponentInHierarchy().AsSingle();
        }
    }
}