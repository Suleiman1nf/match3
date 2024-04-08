using System.Threading;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Gameplay.GameGrid.Commands
{
    public abstract class Command
    {
        public abstract UniTask Execute(CancellationToken cancellationToken);
    }
}