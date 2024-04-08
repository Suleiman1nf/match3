using System;
using System.Collections.Generic;
using System.Threading;
using _Project.Scripts.Gameplay.Cube.Services;
using _Project.Scripts.Gameplay.GameGrid;
using _Project.Scripts.Gameplay.GameGrid.Behaviours;
using _Project.Scripts.Gameplay.GameGrid.Commands;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.GameLevel
{
    public class LevelService : IInitializable, IDisposable
    {
        private readonly SwipeService _swipeService;
        private readonly CubeFactory _cubeFactory;
        private readonly CubeGridMoveService _cubeMoveService;
        private readonly FallService _fallService;
        private readonly GridService _gridService;
        private readonly MatchService _matchService;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private GridModel Grid => _gridService.GridModel;

        private LevelService(
            GridService gridService,
            SwipeService swipeService, 
            CubeFactory cubeFactory,
            CubeGridMoveService cubeGridMoveService,
            FallService fallService,
            MatchService matchService)
        {
            _gridService = gridService;
            _swipeService = swipeService;
            _cubeMoveService = cubeGridMoveService;
            _cubeFactory = cubeFactory;
            _fallService = fallService;
            _matchService = matchService;
        }

        public void Initialize()
        {
            _swipeService.OnSwipe += OnSwipeInputCube;
        }

        public void Dispose()
        {
            _swipeService.OnSwipe -= OnSwipeInputCube;
            _cancellationTokenSource?.Dispose();
        }

        public void Load()
        {
            for (int i = 0; i < Grid.SizeX; i++)
            {
                for (int j = 0; j < Grid.SizeY; j++)
                {
                    if (!Grid.IsEmptyAt(i, j))
                    {
                        _cubeFactory.CreateCube(Grid.Get(i, j), new Vector2Int(i, j));
                    }
                }
            }
        }

        private void OnSwipeInputCube(List<MoveData> moves)
        {
            NormalizeGrid(new Queue<Command>(), moves);
        }

        private void NormalizeGrid(Queue<Command> commands, List<MoveData> swipeMoves)
        {
            List<MoveData> fallMoves = _fallService.ProcessFall();
            List<Vector2Int> matches = _matchService.FindMatches();
            if (fallMoves.Count <= 0 && matches.Count <= 0 && swipeMoves.Count <= 0)
            {
                ExecuteCommands(commands).Forget();
                return;
            }
            DestroyMatches(matches);
            MoveCommand swipeCommand = new MoveCommand(swipeMoves, _cubeFactory, _cubeMoveService);
            MoveCommand fallCommand = new MoveCommand(fallMoves, _cubeFactory, _cubeMoveService);
            DestroyCommand destroyCommand = new DestroyCommand(matches, _cubeFactory);
            commands.Enqueue(swipeCommand);
            commands.Enqueue(fallCommand);
            commands.Enqueue(destroyCommand);
            NormalizeGrid(commands, new List<MoveData>());
        }

        private async UniTask ExecuteCommands(Queue<Command> commands)
        {
            while (commands.Count > 0)
            {
                await commands.Dequeue().Execute(_cancellationTokenSource.Token);
            }
        }

        private void DestroyMatches(List<Vector2Int> matches)
        {
            foreach (Vector2Int pos in matches)
            {
                Grid.Set(pos.x, pos.y, 0);
            }
        }
    }
}