using Zenject;

namespace _Project.Scripts.Gameplay
{
    public class GameLogicInstaller : Installer<GameLogicInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameLogic>().FromNew().AsSingle();
        }
    }
}