using Zenject;

namespace _Project.Scripts.Gameplay.Cube
{
    public class CubeGridMoveServiceInstaller : Installer<CubeGridMoveServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<CubeGridMoveService>().FromComponentInHierarchy().AsSingle();
        }
    }
}