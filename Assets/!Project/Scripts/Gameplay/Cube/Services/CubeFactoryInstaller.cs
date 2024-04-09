using Zenject;

namespace _Project.Scripts.Gameplay.Cube.Services
{
    public class CubeFactoryInstaller : Installer<CubeFactory.Settings, CubeFactoryInstaller>
    {
        private readonly CubeFactory.Settings _settings;

        public CubeFactoryInstaller(CubeFactory.Settings settings)
        {
            _settings = settings;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(_settings).AsSingle();
            Container.Bind<CubeFactory>().FromNew().AsSingle();
        }
    }
}