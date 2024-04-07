using Zenject;

namespace _Project.Scripts.Gameplay.Cube.Services
{
    public class CubeFactoryInstaller : Installer<CubeFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<CubeFactory>().FromComponentInHierarchy().AsSingle();
        }
    }
}