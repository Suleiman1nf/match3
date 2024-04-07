using Zenject;

namespace _Project.Scripts.Gameplay.GameGrid.World
{
    public class WorldGridServiceInstaller : Installer<WorldGridService.Settings, WorldGridServiceInstaller>
    {
        private WorldGridService.Settings _settings;

        public WorldGridServiceInstaller(WorldGridService.Settings settings)
        {
            _settings = settings;
        }

        public override void InstallBindings()
        {
            Container.Bind<WorldGridService>().FromNew().AsSingle().WithArguments(_settings);
        }
    }
}