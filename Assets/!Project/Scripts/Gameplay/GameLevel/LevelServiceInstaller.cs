using Zenject;

namespace _Project.Scripts.Gameplay.GameLevel
{
    public class LevelServiceInstaller : Installer<LevelServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LevelService>().FromNew().AsSingle();
        }
    }
}