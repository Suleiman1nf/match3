using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.GameGrid.World;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Cube.Services
{
    public class CubeFactory : PlaceholderFactory<UnityEngine.Object, CubeController>
    {
        private Settings _settings;

        private List<CubeController> _createdCubes = new List<CubeController>();
        
        private WorldGridService _worldGridService;

        public event Action<CubeController> OnDestroyCube;

        public int CreatedCubesCount => _createdCubes.Count;

        public CubeFactory(WorldGridService worldGridService, Settings settings)
        {
            _worldGridService = worldGridService;
            _settings = settings;
        }

        public CubeController CreateCube(int cubeId, Vector2Int position)
        {
            CubeController prefab = GetCubePrefabById(cubeId);
            CubeController cube = Create(prefab);
            cube.transform.SetParent(_settings.Container);
            cube.transform.position = _worldGridService.GetPosition(position);
            cube.CubeGridData.SetPosition(position);
            cube.transform.localScale *= _worldGridService.Grid.cellSize.x;
            _createdCubes.Add(cube);
            return cube;
        }

        public bool TryGetCube(Vector2Int pos, out CubeController cubeController)
        {
            cubeController = _createdCubes.FirstOrDefault((x)=>x.CubeGridData.GetPosition() == pos);
            return cubeController != null;
        }

        public void DestroyCube(CubeController cubeController)
        {
            _createdCubes.Remove(cubeController);
            OnDestroyCube?.Invoke(cubeController);
            GameObject.Destroy(cubeController.gameObject);
        }
        
        private CubeController GetCubePrefabById(int index)
        {
            if (index < 1 || index > _settings.CubePrefabs.Count)
            {
                throw new Exception($"Cube with id {index} not found");
            }
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