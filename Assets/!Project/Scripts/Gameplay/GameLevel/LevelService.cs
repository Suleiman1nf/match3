using System.Collections.Generic;
using _Project.Scripts.Gameplay.Cube;
using _Project.Scripts.Gameplay.GameGrid;
using _Project.Scripts.Gameplay.GridPlacement;
using _Project.Scripts.Gameplay.Movement;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.GameLevel
{
    public class LevelService : MonoBehaviour
    {
        private CubeSwipeService _cubeSwipeService;
        private GridMovementService _gridMovementService;
        private CubeFactory _cubeFactory;
        private CubeGridMoveService _cubeGridMoveService;
        private FallService _fallService;
        private GridModel _grid;

        [Inject]
        private void Construct(CubeSwipeService cubeSwipeService, 
            CubeFactory cubeFactory,
            GridMovementService gridMovementService,
            CubeGridMoveService cubeGridMoveService,
            FallService fallService)
        {
            _gridMovementService = gridMovementService;
            _cubeSwipeService = cubeSwipeService;
            _cubeGridMoveService = cubeGridMoveService;
            _cubeFactory = cubeFactory;
            _fallService = fallService;
        }

        private void Awake()
        {
            _cubeSwipeService.OnSwipeCube += OnSwipeCube;
        }

        private void OnDestroy()
        {
            _cubeSwipeService.OnSwipeCube -= OnSwipeCube;
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

        private async void OnSwipeCube(CubeGridData cubeData, Vector2Int direction)
        {
            if (_gridMovementService.CanMoveTo(cubeData.GetPosition(), direction))
            {
                Vector2Int origin = cubeData.GetPosition();
                Vector2Int destination = cubeData.GetPosition() + direction;
                _gridMovementService.SwapValues(origin, destination);
                _cubeFactory.TryGetCube(origin, out CubeController firstCube);
                _cubeFactory.TryGetCube(destination, out CubeController secondCube);
                UniTask move1 = _cubeGridMoveService.Move(firstCube, destination, this.GetCancellationTokenOnDestroy());
                UniTask move2 = UniTask.CompletedTask;
                if (secondCube)
                {
                    move2 = _cubeGridMoveService.Move(secondCube, origin, this.GetCancellationTokenOnDestroy());
                }
                await UniTask.WhenAll(move1, move2);
                FallProcess();
            }
        }

        private void FallProcess()
        { 
            List<MoveData> moves = _fallService.ProcessFall();
            List<(CubeController, Vector2Int)> cubes = new List<(CubeController,Vector2Int)>();
            foreach (MoveData moveData in moves)
            {
                if (_cubeFactory.TryGetCube(moveData.Origin, out CubeController cubeController))
                {
                    cubes.Add((cubeController,moveData.Destination));
                }
            }

            foreach ((CubeController cube, Vector2Int destination) pair in cubes)
            {
                _cubeGridMoveService.Move(pair.cube, pair.destination, this.GetCancellationTokenOnDestroy());
            }
        }
    }
}