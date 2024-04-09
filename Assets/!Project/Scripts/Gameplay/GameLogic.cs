using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using _Project.Scripts.Core.Save;
using _Project.Scripts.Gameplay.Cube;
using _Project.Scripts.Gameplay.Cube.Services;
using _Project.Scripts.Gameplay.GameGrid;
using _Project.Scripts.Gameplay.GameGrid.Behaviours;
using _Project.Scripts.Gameplay.GameGrid.Commands;
using _Project.Scripts.Gameplay.GameLevel;
using _Project.Scripts.Gameplay.InputManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay
{
    public class GameLogic : IInitializable, IDisposable
    {
        private readonly SaveService _saveService;
        private readonly LevelLoadService _levelLoadService;
        private readonly SwipeInputService _swipeInputService;
        private readonly CubeFactory _cubeFactory;
        private readonly SwapService _swapService;
        private readonly FallService _fallService;
        private readonly MatchService _matchService;
        private readonly CommandFactory _commandFactory;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly List<CommandsContainer> _runningCommands = new List<CommandsContainer>();
        public GridModel Grid { get; private set; }

        private GameLogic(
            SaveService saveService,
            LevelLoadService levelLoadService,
            SwipeInputService swipeInputService, 
            CubeFactory cubeFactory,
            FallService fallService,
            MatchService matchService,
            SwapService swapService,
            CommandFactory commandFactory)
        {
            _levelLoadService = levelLoadService;
            _swipeInputService = swipeInputService;
            _swapService = swapService;
            _cubeFactory = cubeFactory;
            _fallService = fallService;
            _matchService = matchService;
            _commandFactory = commandFactory;
            _saveService = saveService;
        }

        public void Initialize()
        {
            _swipeInputService.OnSwipe += OnSwipeInput;
        }

        public void Dispose()
        {
            _swipeInputService.OnSwipe -= OnSwipeInput;
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }

        public void Start()
        {
            Grid = _levelLoadService.InitialGrid;
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
                _saveService.GameSave.GridData = GridParser.ToData(Grid);
                ExecuteCommands(commandsContainer).Forget();
            }
        }

        private void ExecuteSwapOnGrid(Vector2Int origin, Vector2Int direction, GridModel grid, CommandsContainer commandsContainer)
        {
            List<MoveData> swapMoves = _swapService.Swap(grid, origin, direction);
            MoveCommand swipeCommand = _commandFactory.Create<MoveCommand>().Init(swapMoves, MoveType.Side);
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
            
            MoveCommand fallCommand = _commandFactory.Create<MoveCommand>().Init(fallMoves, MoveType.Fall);
            DestroyCommand destroyCommand = _commandFactory.Create<DestroyCommand>().Init(matches);
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