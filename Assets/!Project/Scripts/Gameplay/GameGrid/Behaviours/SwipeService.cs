using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Cube;
using _Project.Scripts.Gameplay.InputManagement;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.GameGrid.Behaviours
{
    public class SwipeService : IInitializable, IDisposable
    {
        private readonly SwipeInputService _swipeInputService;
        private readonly GridService _gridService;
        public event Action<List<MoveData>> OnSwipe;

        public SwipeService(SwipeInputService swipeInputService, GridService gridService)
        {
            _swipeInputService = swipeInputService;
            _gridService = gridService;
        }
        
        public void Initialize()
        {
            _swipeInputService.OnSwipe += OnSwipeInput;
        }

        public void Dispose()
        {
            _swipeInputService.OnSwipe -= OnSwipeInput;
        }

        private void OnSwipeInput(ISwipeable swipeable, Vector2Int direction)
        {
            CubeController cubeController = swipeable as CubeController;
            if (cubeController == null)
            {
                return;
            }
            
            Vector2Int origin = cubeController.CubeGridData.GetPosition();
            Vector2Int destination = origin + direction;
            if (!_gridService.InAir(origin) 
                && !_gridService.GridModel.IsEmptyAt(origin) 
                && _gridService.CanMoveTo(origin, direction))
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