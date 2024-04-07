using Zenject;

namespace _Project.Scripts.Gameplay.GameGrid.Movement
{
    public class SwipeServiceInstaller : Installer<SwipeServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<SwipeService>().FromComponentInHierarchy().AsSingle();
        }
    }
}