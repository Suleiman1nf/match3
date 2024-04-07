using System.Collections.Generic;
using System.Threading;
using _Project.Scripts.Gameplay.Cube;
using _Project.Scripts.Gameplay.Cube.Services;
using _Project.Scripts.Gameplay.GameGrid;
using _Project.Scripts.Gameplay.GameGrid.Movement;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.GameLevel
{
    public class LevelService : MonoBehaviour
    {
        private SwipeService _swipeService;
        private CubeFactory _cubeFactory;
        private CubeGridMoveService _cubeMoveService;
        private FallService _fallService;
        private GridModel _grid;

        [Inject]
        private void Construct(SwipeService swipeService, 
            CubeFactory cubeFactory,
            CubeGridMoveService cubeGridMoveService,
            FallService fallService)
        {
            _swipeService = swipeService;
            _cubeMoveService = cubeGridMoveService;
            _cubeFactory = cubeFactory;
            _fallService = fallService;
        }

        private void Awake()
        {
            _swipeService.OnSwipe += OnSwipeInputCube;
        }

        private void OnDestroy()
        {
            _swipeService.OnSwipe -= OnSwipeInputCube;
        }

        public void Load(GridModel grid)
        {
            _grid = grid;
            
            for (int i = 0; i < _grid.SizeX; i++)
            {
                for (int j = 0; j < _grid.SizeY; j++)
                {
                    if (!_grid.IsEmptyAt(i, j))
                    {
                        _cubeFactory.CreateCube(_grid.Get(i, j), new Vector2Int(i, j));
                    }
                }
            }
        }

        private async void OnSwipeInputCube(List<MoveData> moves)
        {
            await MoveAll(moves, this.GetCancellationTokenOnDestroy());
            FallProcess();
        }

        private void FallProcess()
        {
            List<MoveData> moves = _fallService.ProcessFall();
            MoveAll(moves, this.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTask MoveAll(List<MoveData> moves, CancellationToken cancellationToken)
        {
            List<CubeController> cubes = new List<CubeController>();
            List<UniTask> tasks = new List<UniTask>();
            foreach (MoveData moveData in moves)
            {
                _cubeFactory.TryGetCube(moveData.Origin, out CubeController cubeController);
                cubes.Add(cubeController);
            }
            
            for (var index = 0; index < cubes.Count; index++)
            {
                CubeController cubeController = cubes[index];
                if (cubeController != null)
                {
                    UniTask task = _cubeMoveService.Move(cubeController, moves[index].Destination,
                        cancellationToken);
                    tasks.Add(task);
                }
            }

            await UniTask.WhenAll(tasks);
        }
    }
}