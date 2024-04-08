using System.Threading;
using _Project.Scripts.Core;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Cube
{
    public enum MoveType
    {
        Side,
        Fall
    }
    public class CubeMovement : MonoBehaviour
    {
        private GameSettings _gameSettings;

        [Inject]
        private void Construct(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        public async UniTask MoveAsync(MoveType moveType, Vector3 destination, CancellationToken cancellationToken)
        {
            float duration = moveType == MoveType.Side ? _gameSettings.SideMoveDuration : _gameSettings.FallDuration;
            Ease ease = moveType == MoveType.Side ? _gameSettings.SideMoveEase : _gameSettings.FallEase;
            await transform.DOMove(destination, duration).SetEase(ease).WithCancellation(cancellationToken);
        }
    }
}