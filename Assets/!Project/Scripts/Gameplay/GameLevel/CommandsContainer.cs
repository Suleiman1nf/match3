using System.Collections.Generic;
using _Project.Scripts.Gameplay.GameGrid.Commands;
using UnityEngine;

namespace _Project.Scripts.Gameplay.GameLevel
{
    public class CommandsContainer
    {
        public Queue<Command> Commands { get; private set; }
        public List<Vector2Int> InvolvedPositions { get; private set; }

        public CommandsContainer() : this(new Queue<Command>(), new List<Vector2Int>())
        {
        }

        public CommandsContainer(Queue<Command> commands, List<Vector2Int> involvedPositions)
        {
            Commands = commands;
            InvolvedPositions = involvedPositions;
        }
    }
}