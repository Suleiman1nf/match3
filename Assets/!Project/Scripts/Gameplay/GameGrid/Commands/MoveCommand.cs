using System.Collections.Generic;
using System.Threading;
using _Project.Scripts.Gameplay.Cube;
using _Project.Scripts.Gameplay.Cube.Services;
using _Project.Scripts.Gameplay.GameGrid.Behaviours;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Gameplay.GameGrid.Commands
{
    public class MoveCommand : Command
    {
        private readonly CubeFactory _cubeFactory;
        private readonly CubeGridMoveService _cubeGridMoveService;
        
        private List<MoveData> _moves;
        private MoveType _moveType;
        
        private MoveCommand(CubeFactory cubeFactory, CubeGridMoveService cubeGridMoveService)
        {
            _cubeFactory = cubeFactory;
            _cubeGridMoveService = cubeGridMoveService;
        }

        public MoveCommand Init(List<MoveData> moves, MoveType moveType)
        {
            _moves = moves;
            _moveType = moveType;
            return this;
        }

        public override async UniTask Execute(CancellationToken cancellationToken)
        {
            List<CubeController> cubes = new List<CubeController>();
            List<UniTask> tasks = new List<UniTask>();
            foreach (MoveData moveData in _moves)
            {
                _cubeFactory.TryGetCube(moveData.Origin, out CubeController cubeController);
                cubes.Add(cubeController);
            }
            
            for (var index = 0; index < cubes.Count; index++)
            {
                CubeController cubeController = cubes[index];
                if (cubeController != null)
                {
                    UniTask task = _cubeGridMoveService.Move(cubeController, _moves[index].Destination,_moveType,
                        cancellationToken);
                    tasks.Add(task);
                }
            }

            await UniTask.WhenAll(tasks).AttachExternalCancellation(cancellationToken);
        }
    }
}