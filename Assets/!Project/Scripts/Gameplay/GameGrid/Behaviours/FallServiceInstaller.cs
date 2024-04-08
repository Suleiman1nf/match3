using Zenject;

namespace _Project.Scripts.Gameplay.GameGrid.Behaviours
{
    public class FallServiceInstaller : Installer<FallServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<FallService>().FromNew().AsSingle();
        }
    }
}