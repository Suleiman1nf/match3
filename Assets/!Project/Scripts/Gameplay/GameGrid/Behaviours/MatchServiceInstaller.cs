using Zenject;

namespace _Project.Scripts.Gameplay.GameGrid.Behaviours
{
    public class MatchServiceInstaller : Installer<MatchServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<MatchService>().FromNew().AsSingle();
        }
    }
}