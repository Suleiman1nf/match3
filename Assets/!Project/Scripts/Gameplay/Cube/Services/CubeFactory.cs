using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.GameGrid.World;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Cube.Services
{
    public class CubeFactory
    {
        private Settings _settings;

        private List<CubeController> _createdCubes = new List<CubeController>();
        
        private WorldGridService _worldGridService;

        [Inject]
        public void Construct(WorldGridService worldGridService, Settings settings)
        {
            _worldGridService = worldGridService;
            _settings = settings;
        }

        public CubeController CreateCube(int cubeId, Vector2Int position)
        {
            CubeController prefab = GetCubePrefabById(cubeId);
            CubeController cube = GameObject.Instantiate(prefab, _settings.Container);
            cube.transform.position = _worldGridService.GetPosition(position);
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
            return _settings.CubePrefabs[index-1];
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Transform Container { get; private set; }
            [field: SerializeField] public List<CubeController> CubePrefabs { get; private set; }
        }
    }
}