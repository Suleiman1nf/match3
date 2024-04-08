using System;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.InputManagement
{
    public class SwipeInputService : IInitializable, ITickable
    {
        private const float MinSwipeDistance = 40f;
        private Camera _camera;

        private Vector3 _startPosition;
        private ISwipeable _swipeable;
        public event Action<ISwipeable, Vector2Int> OnSwipe;
        
        public void Initialize()
        {
            _camera = Camera.main;
        }
        
        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _swipeable = null;
                RaycastHit2D rayHit = Physics2D.GetRayIntersection(_camera.ScreenPointToRay(Input.mousePosition));
                if (rayHit)
                {
                    if (rayHit.transform.TryGetComponent(out ISwipeable swipeable))
                    {
                        _startPosition = Input.mousePosition;
                        _swipeable = swipeable;
                    }
                }
            }
            if (Input.GetMouseButtonUp(0) && _swipeable != null)
            {
                float distance = Vector3.Distance(Input.mousePosition, _startPosition);
                Vector3 direction =  Input.mousePosition - _startPosition;
                if (distance > MinSwipeDistance && _swipeable.CanSwipe)
                {
                    OnSwipe?.Invoke(_swipeable, GetSwipeDirection(direction));
                }
                _swipeable = null;
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