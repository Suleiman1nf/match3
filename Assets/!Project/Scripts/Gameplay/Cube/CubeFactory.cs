using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Cube
{
    public class CubeFactory : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private List<GameObject> _cubes;
        public List<GameObject> Cubes => _cubes;

        public GameObject CreateCube(int index)
        {
            GameObject prefab = GetCubePrefabById(index);
            GameObject obj = Instantiate(prefab, _container);
            return obj;
        }
        
        private GameObject GetCubePrefabById(int index)
        {
            return _cubes[index-1];
        }
    }
}