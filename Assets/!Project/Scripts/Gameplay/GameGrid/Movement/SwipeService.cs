using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Cube;
using _Project.Scripts.Gameplay.Cube.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.GameGrid.Movement
{
    public class SwipeService : IInitializable, IDisposable
    {
        private readonly CubeSwipeInputService _cubeSwipeInputService;
        private readonly GridService _gridService;

        public event Action<List<MoveData>> OnSwipe;

        public SwipeService(CubeSwipeInputService cubeSwipeInputService, GridService gridService)
        {
            _cubeSwipeInputService = cubeSwipeInputService;
            _gridService = gridService;
        }
        
        public void Initialize()
        {
            _cubeSwipeInputService.OnSwipeCube += OnSwipeCube;
        }

        public void Dispose()
        {
            _cubeSwipeInputService.OnSwipeCube -= OnSwipeCube;
        }

        private void OnSwipeCube(CubeGridData cubeGridData, Vector2Int direction)
        {
            Vector2Int origin = cubeGridData.GetPosition();
            Vector2Int destination = origin + direction;
            if (_gridService.CanMoveTo(origin, direction))
            {
                List<MoveData> moves = new List<MoveData>();
                _gridService.SwapValues(origin, destination);
                moves.Add(new MoveData(origin, destination));
                moves.Add(new MoveData(destination, origin));
                OnSwipe?.Invoke(moves);
            }
        }
    }
}