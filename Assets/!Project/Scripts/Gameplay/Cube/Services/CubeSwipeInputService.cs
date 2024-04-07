using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Cube.Services
{
    public class CubeSwipeInputService : MonoBehaviour
    {
        private const float MinSwipeDistance = 40f;
        private Camera _camera;

        private Vector3 _startPosition;
        private CubeGridData _swipingCubeData;

        public event Action<CubeGridData, Vector2Int> OnSwipeCube;

        private void Awake()
        {
            _camera = Camera.main;
        }
        
        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _swipingCubeData = null;
                RaycastHit2D rayHit = Physics2D.GetRayIntersection(_camera.ScreenPointToRay(Input.mousePosition));
                if (rayHit)
                {
                    if (rayHit.transform.TryGetComponent(out CubeGridData gridData))
                    {
                        _startPosition = Input.mousePosition;
                        _swipingCubeData = gridData;
                    }
                }
            }
            if (Input.GetMouseButtonUp(0) && _swipingCubeData != null)
            {
                float distance = Vector3.Distance(Input.mousePosition, _startPosition);
                Vector3 direction =  Input.mousePosition - _startPosition;
                if (distance > MinSwipeDistance)
                {
                    OnSwipeCube?.Invoke(_swipingCubeData, GetSwipeDirection(direction));
                }
                _swipingCubeData = null;
            }
        }

        private Vector2Int GetSwipeDirection(Vector2 direction)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                return direction.x > 0 ? new Vector2Int(1, 0) : new Vector2Int(-1, 0);
            }
            else
            {
                return direction.y > 0 ? new Vector2Int(0, 1) : new Vector2Int(0, -1);
            }
        }
    }
}