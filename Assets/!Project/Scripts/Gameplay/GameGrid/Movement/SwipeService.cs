using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Cube;
using _Project.Scripts.Gameplay.Cube.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.GameGrid.Movement
{
    public class SwipeService : MonoBehaviour
    {
        private CubeSwipeInputService _cubeSwipeInputService;
        private GridMovementService _gridMovementService;

        public event Action<List<MoveData>> OnSwipe;
        
        [Inject]
        private void Construct(CubeSwipeInputService cubeSwipeInputService, GridMovementService gridMovementService)
        {
            _cubeSwipeInputService = cubeSwipeInputService;
            _gridMovementService = gridMovementService;
        }

        private void Awake()
        {
            _cubeSwipeInputService.OnSwipeCube += OnSwipeCube;
        }

        private void OnDestroy()
        {
            _cubeSwipeInputService.OnSwipeCube -= OnSwipeCube;
        }

        private void OnSwipeCube(CubeGridData cubeGridData, Vector2Int direction)
        {
            Vector2Int origin = cubeGridData.GetPosition();
            Vector2Int destination = origin + direction;
            if (_gridMovementService.CanMoveTo(origin, direction))
            {
                List<MoveData> moves = new List<MoveData>();
                _gridMovementService.SwapValues(origin, destination);
                moves.Add(new MoveData(origin, destination));
                moves.Add(new MoveData(destination, origin));
                OnSwipe?.Invoke(moves);
            }
        }
    }
}