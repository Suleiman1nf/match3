using System;
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
    public class LevelService : IInitializable, IDisposable
    {
        private readonly SwipeService _swipeService;
        private readonly CubeFactory _cubeFactory;
        private readonly CubeGridMoveService _cubeMoveService;
        private readonly FallService _fallService;
        private readonly GridService _gridService;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        
        private GridModel Grid => _gridService.GridModel;

        private LevelService(
            GridService gridService,
            SwipeService swipeService, 
            CubeFactory cubeFactory,
            CubeGridMoveService cubeGridMoveService,
            FallService fallService)
        {
            _gridService = gridService;
            _swipeService = swipeService;
            _cubeMoveService = cubeGridMoveService;
            _cubeFactory = cubeFactory;
            _fallService = fallService;
        }

        public void Initialize()
        {
            _swipeService.OnSwipe += OnSwipeInputCube;
        }

        public void Dispose()
        {
            _swipeService.OnSwipe -= OnSwipeInputCube;
            _cancellationTokenSource?.Dispose();
        }

        public void Load()
        {
            for (int i = 0; i < Grid.SizeX; i++)
            {
                for (int j = 0; j < Grid.SizeY; j++)
                {
                    if (!Grid.IsEmptyAt(i, j))
                    {
                        _cubeFactory.CreateCube(Grid.Get(i, j), new Vector2Int(i, j));
                    }
                }
            }
        }

        private async void OnSwipeInputCube(List<MoveData> moves)
        {
            await MoveCubes(moves, _cancellationTokenSource.Token);
            FallProcess();
        }

        private void FallProcess()
        {
            List<MoveData> moves = _fallService.ProcessFall();
            MoveCubes(moves, _cancellationTokenSource.Token).Forget();
        }

        private async UniTask MoveCubes(List<MoveData> moves, CancellationToken cancellationToken)
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