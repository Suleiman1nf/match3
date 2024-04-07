using Zenject;

namespace _Project.Scripts.Gameplay.Cube.Services
{
    public class CubeGridMoveServiceInstaller : Installer<CubeGridMoveServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<CubeGridMoveService>().FromNew().AsSingle();
        }
    }
}