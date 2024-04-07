using Zenject;

namespace _Project.Scripts.Gameplay.GameGrid.Movement
{
    public class GridServiceInstaller : Installer<GridServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<GridService>().FromNew().AsSingle();
        }
    }
}