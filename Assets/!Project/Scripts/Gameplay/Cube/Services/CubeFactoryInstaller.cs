using Zenject;

namespace _Project.Scripts.Gameplay.Cube.Services
{
    public class CubeFactoryInstaller : Installer<CubeFactory.Settings, CubeFactoryInstaller>
    {
        private CubeFactory.Settings _settings;

        public CubeFactoryInstaller(CubeFactory.Settings settings)
        {
            _settings = settings;
        }

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CubeFactory>().FromNew().AsSingle().WithArguments(_settings);
        }
    }
}