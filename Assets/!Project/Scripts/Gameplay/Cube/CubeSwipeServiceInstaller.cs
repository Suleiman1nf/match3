using Zenject;

namespace _Project.Scripts.Gameplay.Cube
{
    public class CubeSwipeServiceInstaller : Installer<CubeSwipeServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<CubeSwipeService>().FromComponentInHierarchy().AsSingle();
        }
    }
}