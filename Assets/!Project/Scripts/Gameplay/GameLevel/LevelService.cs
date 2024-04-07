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
        private GridPlacementService _gridPlacementService;
        private CubeSwipeService _cubeSwipeService;
        private GridMovementService _gridMovementService;
        private CubeFactory _cubeFactory;
        private GridModel _grid;

        [Inject]
        private void Construct(CubeSwipeService cubeSwipeService, 
            GridPlacementService gridPlacementService, 
            CubeFactory cubeFactory,
            GridMovementService gridMovementService)
        {
            _gridMovementService = gridMovementService;
            _gridPlacementService = gridPlacementService;
            _cubeSwipeService = cubeSwipeService;
            _cubeFactory = cubeFactory;
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

        private void OnSwipeCube(CubeGridData cubeData, Vector2Int direction)
        {
            if (_gridMovementService.CanMoveTo(cubeData.GetPosition(), direction))
            {
                Vector2Int origin = cubeData.GetPosition();
                Vector2Int destination = cubeData.GetPosition() + direction;
                _gridMovementService.SwapValues(origin, destination);
                _cubeFactory.TryGetCube(origin, out CubeController firstCube);
                _cubeFactory.TryGetCube(destination, out CubeController secondCube);
                firstCube.CubeMovement.MoveAsync(
                    _gridPlacementService.GetPosition(destination), this.GetCancellationTokenOnDestroy()).Forget();
                firstCube.CubeGridData.SetPosition(destination);
                if (secondCube)
                {
                    secondCube.CubeGridData.SetPosition(origin);
                    secondCube.CubeMovement.MoveAsync(
                        _gridPlacementService.GetPosition(origin), this.GetCancellationTokenOnDestroy()).Forget();
                }
            }
        }
    }
}