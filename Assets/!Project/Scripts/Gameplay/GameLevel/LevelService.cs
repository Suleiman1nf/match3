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

        private List<CommandsContainer> _currentCommands = new List<CommandsContainer>();
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

            CommandsContainer commandsContainer = new CommandsContainer();
            GridModel copyGrid = Grid.Clone();
            
            Vector2Int origin = cubeController.CubeGridData.GetPosition();
            List<MoveData> swapMoves = _swapService.Swap(copyGrid, origin, direction);
            if (TryChangeGrid(copyGrid, commandsContainer, swapMoves))
            {
                Grid = copyGrid;
                ExecuteCommands(commandsContainer).Forget();
            }
        }

        private bool TryChangeGrid(GridModel grid, CommandsContainer commandsContainer, List<MoveData> swipeMoves)
        {
            List<MoveData> fallMoves = _fallService.ProcessFall(grid);
            List<Vector2Int> matches = _matchService.FindMatches(grid);
            if (fallMoves.Count <= 0 && matches.Count <= 0 && swipeMoves.Count <= 0)
            {
                // if we involve positions that are currently in chain of commands to execute we don't allow it
                foreach (CommandsContainer currentCommand in _currentCommands)
                {
                    if (commandsContainer.InvolvedPositions.Intersect(currentCommand.InvolvedPositions).Any())
                    {
                        return false;
                    }
                }
                return true;
            }
            DestroyMatches(grid, matches);
            MoveCommand swipeCommand = new MoveCommand(swipeMoves, _cubeFactory, _cubeMoveService);
            MoveCommand fallCommand = new MoveCommand(fallMoves, _cubeFactory, _cubeMoveService);
            DestroyCommand destroyCommand = new DestroyCommand(matches, _cubeFactory);
            commandsContainer.InvolvedPositions.AddRange(matches);
            commandsContainer.InvolvedPositions.AddRange(fallMoves.Select((x)=>x.Destination).ToList());
            commandsContainer.InvolvedPositions.AddRange(fallMoves.Select((x)=>x.Origin).ToList());
            commandsContainer.InvolvedPositions.AddRange(swipeMoves.Select((x)=>x.Origin).ToList());
            commandsContainer.InvolvedPositions.AddRange(swipeMoves.Select((x)=>x.Destination).ToList());
            commandsContainer.Commands.Enqueue(swipeCommand);
            commandsContainer.Commands.Enqueue(fallCommand);
            commandsContainer.Commands.Enqueue(destroyCommand);
            return TryChangeGrid(grid, commandsContainer, new List<MoveData>());
        }

        private async UniTask ExecuteCommands(CommandsContainer commandsContainer)
        {
            _currentCommands.Add(commandsContainer);
            while (commandsContainer.Commands.Count > 0)
            {
                await commandsContainer.Commands.Dequeue().Execute(_cancellationTokenSource.Token);
            }
            _currentCommands.Remove(commandsContainer);
        }

        private void DestroyMatches(GridModel gridModel, List<Vector2Int> matches)
        {
            foreach (Vector2Int pos in matches)
            {
                gridModel.Set(pos.x, pos.y, 0);
            }
        }
    }
}