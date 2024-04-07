using _Project.Scripts.Gameplay.Cube;
using _Project.Scripts.Gameplay.GameGrid;
using _Project.Scripts.Gameplay.GridPlacement;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.GameLevel
{
    public class LevelService : MonoBehaviour
    {
        private GridPlacementService _gridPlacementService;
        private CubeFactory _cubeFactory;
        private GridModel _grid;

        [Inject]
        public void Construct(GridPlacementService gridPlacementService, CubeFactory cubeFactory)
        {
            _gridPlacementService = gridPlacementService;
            _cubeFactory = cubeFactory;
        }

        public void Load(GridModel grid)
        {
            _grid = grid;
            _gridPlacementService.Init(_grid.SizeX, _grid.SizeY);

            for (int i = 0; i < _grid.SizeX; i++)
            {
                for (int j = 0; j < _grid.SizeY; j++)
                {
                    Vector3 position = _gridPlacementService.GetPosition(i, j);
                    if (_grid.Get(i, j) != 0)
                    {
                        GameObject cube = _cubeFactory.CreateCube(_grid.Get(i, j));
                        cube.transform.position = position;
                    }
                }
            }
        }
    }
}