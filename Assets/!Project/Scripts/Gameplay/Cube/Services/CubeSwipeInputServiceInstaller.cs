using Zenject;

namespace _Project.Scripts.Gameplay.Cube.Services
{
    public class CubeSwipeInputServiceInstaller : Installer<CubeSwipeInputServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CubeSwipeInputService>().FromNew().AsSingle();
        }
    }
}