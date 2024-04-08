using System.Threading;
using _Project.Scripts.Gameplay.GameGrid.World;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Cube.Services
{
    public class CubeGridMoveService
    {
        private readonly WorldGridService _worldGridService;

        private CubeGridMoveService(WorldGridService worldGridService)
        {
            _worldGridService = worldGridService;
        }

        public async UniTask Move(CubeController cubeController, Vector2Int destination, CancellationToken cancellationToken)
        {
            Vector3 position = _worldGridService.GetPosition(destination);
            cubeController.CubeGridData.SetPosition(destination);
            await cubeController.CubeMovement.MoveAsync(position, cancellationToken);
        }
    }
}