using Zenject;

namespace _Project.Scripts.Gameplay.GameGrid.Commands
{
    public class CommandFactoryInstaller : Installer<CommandFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<CommandFactory>().FromNew().AsSingle();
        }
    }
}