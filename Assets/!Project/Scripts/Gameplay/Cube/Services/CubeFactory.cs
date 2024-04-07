using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.GameGrid.Placement;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Cube.Services
{
    public class CubeFactory : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private List<CubeController> _cubes;

        private List<CubeController> _createdCubes = new List<CubeController>();
        
        private GridPlacementService _gridPlacementService;

        [Inject]
        public void Construct(GridPlacementService gridPlacementService)
        {
            _gridPlacementService = gridPlacementService;
        }

        public CubeController CreateCube(int index, Vector2Int position)
        {
            CubeController prefab = GetCubePrefabById(index);
            CubeController cube = Instantiate(prefab, _container);
            cube.transform.position = _gridPlacementService.GetPosition(position);
            cube.CubeGridData.SetPosition(position);
            _createdCubes.Add(cube);
            return cube;
        }

        public bool TryGetCube(Vector2Int pos, out CubeController cubeController)
        {
            cubeController = _createdCubes.FirstOrDefault((x)=>x.CubeGridData.GetPosition() == pos);
            return cubeController != null;
        }
        
        private CubeController GetCubePrefabById(int index)
        {
            return _cubes[index-1];
        }
    }
}