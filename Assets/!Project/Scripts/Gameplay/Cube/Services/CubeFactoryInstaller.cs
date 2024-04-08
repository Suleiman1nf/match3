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
            Container.BindInstance(_settings).AsSingle();
            Container.BindFactory<UnityEngine.Object, CubeController, CubeFactory>().FromFactory<PrefabFactory<CubeController>>();
        }
    }
}