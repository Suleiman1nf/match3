using System.Threading;
using _Project.Scripts.Gameplay.GameGrid.Placement;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Cube.Services
{
    public class CubeGridMoveService : MonoBehaviour
    {
        private GridPlacementService _gridPlacementService;

        [Inject]
        private void Construct(GridPlacementService gridPlacementService)
        {
            _gridPlacementService = gridPlacementService;
        }

        public async UniTask Move(CubeController cubeController, Vector2Int destination, CancellationToken cancellationToken)
        {
            Vector3 position = _gridPlacementService.GetPosition(destination);
            cubeController.CubeGridData.SetPosition(destination);
            await cubeController.CubeMovement.MoveAsync(position, cancellationToken);
        }
    }
}