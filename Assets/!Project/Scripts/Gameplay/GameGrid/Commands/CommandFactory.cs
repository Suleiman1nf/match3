using Zenject;

namespace _Project.Scripts.Gameplay.GameGrid.Commands
{
    public class CommandFactory
    {
        private readonly DiContainer _diContainer;

        public CommandFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public T Create<T>() where T : Command
        {
            return _diContainer.Instantiate<T>();
        }
    }
}