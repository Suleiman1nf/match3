using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using _Project.Scripts.Gameplay.Cube;
using _Project.Scripts.Gameplay.Cube.Services;
using _Project.Scripts.Gameplay.GameGrid;
using _Project.Scripts.Gameplay.GameGrid.Behaviours;
using _Project.Scripts.Gameplay.GameGrid.Commands;
using _Project.Scripts.Gameplay.InputManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.GameLevel
{
    public class LevelService : IInitializable, IDisposable
    {
        private readonly SwipeInputService _swipeInputService;
        private readonly CubeFactory _cubeFactory;
        private readonly CubeGridMoveService _cubeMoveService;
        private readonly SwapService _swapService;
        private readonly FallService _fallService;
        private readonly MatchService _matchService;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private List<CommandsContainer> _runningCommands = new List<CommandsContainer>();
        public GridModel Grid { get; private set; }

        private LevelService(
            SwipeInputService swipeInputService, 
            CubeFactory cubeFactory,
            CubeGridMoveService cubeGridMoveService,
            FallService fallService,
            MatchService matchService,
            SwapService swapService)
        {
            _swipeInputService = swipeInputService;
            _swapService = swapService;
            _cubeMoveService = cubeGridMoveService;
            _cubeFactory = cubeFactory;
            _fallService = fallService;
            _matchService = matchService;
        }

        public void Initialize()
        {
            _swipeInputService.OnSwipe += OnSwipeInput;
        }

        public void Dispose()
        {
            _swipeInputService.OnSwipe -= OnSwipeInput;
            _cancellationTokenSource?.Dispose();
        }

        public void Load(GridModel grid)
        {
            Grid = grid;
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

        private void OnSwipeInput(ISwipeable swipeable, Vector2Int direction)
        {
            CubeController cubeController = swipeable as CubeController;
            if (cubeController == null)
            {
                return;
            }

            Vector2Int origin = cubeController.CubeGridData.GetPosition();
            CommandsContainer commandsContainer = new CommandsContainer();
            
            GridModel copyGrid = Grid.Clone();

            ExecuteSwapOnGrid(origin, direction, copyGrid, commandsContainer);

            if (TryChangeGrid(copyGrid, commandsContainer))
            {
                Grid = copyGrid;
                ExecuteCommands(commandsContainer).Forget();
            }
        }

        private void ExecuteSwapOnGrid(Vector2Int origin, Vector2Int direction, GridModel grid, CommandsContainer commandsContainer)
        {
            List<MoveData> swapMoves = _swapService.Swap(grid, origin, direction);
            MoveCommand swipeCommand = new MoveCommand(swapMoves, MoveType.Side, _cubeFactory, _cubeMoveService);
            commandsContainer.InvolvedPositions.AddRange(swapMoves.Select((x)=>x.Origin).ToList());
            commandsContainer.InvolvedPositions.AddRange(swapMoves.Select((x)=>x.Destination).ToList());
            commandsContainer.Commands.Enqueue(swipeCommand);
        }

        private bool TryChangeGrid(GridModel grid, CommandsContainer commandsContainer)
        {
            List<MoveData> fallMoves = _fallService.ProcessFall(grid);
            List<Vector2Int> matches = _matchService.FindMatches(grid);
            _matchService.DestroyMatches(grid, matches);
            if (fallMoves.Count <= 0 && matches.Count <= 0)
            {
                // if we involve positions that are currently in running commands we don't allow it
                return !HasIntersectingPositions(commandsContainer);
            }
            
            MoveCommand fallCommand = new MoveCommand(fallMoves, MoveType.Fall, _cubeFactory, _cubeMoveService);
            DestroyCommand destroyCommand = new DestroyCommand(matches, _cubeFactory);
            commandsContainer.Commands.Enqueue(fallCommand);
            commandsContainer.Commands.Enqueue(destroyCommand);
            
            commandsContainer.InvolvedPositions.AddRange(matches);
            commandsContainer.InvolvedPositions.AddRange(fallMoves.Select((x)=>x.Destination).ToList());
            commandsContainer.InvolvedPositions.AddRange(fallMoves.Select((x)=>x.Origin).ToList());
            
            return TryChangeGrid(grid, commandsContainer);
        }

        private async UniTask ExecuteCommands(CommandsContainer commandsContainer)
        {
            _runningCommands.Add(commandsContainer);
            while (commandsContainer.Commands.Count > 0)
            {
                await commandsContainer.Commands.Dequeue().Execute(_cancellationTokenSource.Token);
            }
            _runningCommands.Remove(commandsContainer);
        }

        private bool HasIntersectingPositions(CommandsContainer commandsContainer)
        {
            foreach (CommandsContainer currentCommand in _runningCommands)
            {
                if (commandsContainer.InvolvedPositions.Intersect(currentCommand.InvolvedPositions).Any())
                {
                    return true;
                }
            }
            return false;
        }
    }
}