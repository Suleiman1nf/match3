using Zenject;

namespace _Project.Scripts.Gameplay.GridPlacement
{
    public class GridPlacementServiceInstaller : Installer<GridPlacementServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<GridPlacementService>().FromComponentInHierarchy().AsSingle();
        }
    }
}