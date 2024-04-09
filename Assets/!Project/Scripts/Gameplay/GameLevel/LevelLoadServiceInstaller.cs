using Zenject;

namespace _Project.Scripts.Gameplay.GameLevel
{
    public class LevelLoadServiceInstaller : Installer<LevelLoadServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<LevelLoadService>().FromNew().AsSingle();
        }
    }
}