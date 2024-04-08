using System.Collections.Generic;
using System.Threading;
using _Project.Scripts.Gameplay.Cube;
using _Project.Scripts.Gameplay.Cube.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Gameplay.GameGrid.Commands
{
    public class DestroyCommand : Command
    {
        private CubeFactory _cubeFactory;
        private List<Vector2Int> _matches;

        public DestroyCommand(List<Vector2Int> matches, CubeFactory cubeFactory)
        {
            _matches = matches;
            _cubeFactory = cubeFactory;
        }

        public override async UniTask Execute(CancellationToken cancellationToken)
        {
            List<UniTask> destroyTasks = new List<UniTask>();
            List<CubeController> cubes = new List<CubeController>();
            foreach (Vector2Int pos in _matches)
            {
                if (_cubeFactory.TryGetCube(pos, out CubeController cubeController))
                {
                    cubeController.CanSwipe = false;
                    destroyTasks.Add(cubeController.CubeAnimation.PlayDeath(cancellationToken));
                    cubes.Add(cubeController);
                }
            }

            await UniTask.WhenAll(destroyTasks).AttachExternalCancellation(cancellationToken);

            foreach (CubeController cubeController in cubes)
            {
                _cubeFactory.DestroyCube(cubeController);
            }
        }
    }
}