using System.Threading;
using _Project.Scripts.Core;
using _Project.Scripts.Core.Audio;
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
        private AudioService _audioService;

        [Inject]
        private void Construct(GameSettings gameSettings, AudioService audioService)
        {
            _gameSettings = gameSettings;
            _audioService = audioService;
        }

        public async UniTask MoveAsync(MoveType moveType, Vector3 destination, CancellationToken cancellationToken)
        {
            float duration = moveType == MoveType.Side ? _gameSettings.SideMoveDuration : _gameSettings.FallDuration;
            Ease ease = moveType == MoveType.Side ? _gameSettings.SideMoveEase : _gameSettings.FallEase;
            _audioService.PlayEffect(_audioService.AudioData.Move, 0.5f);
            await transform.DOMove(destination, duration).SetEase(ease).WithCancellation(cancellationToken);
        }
    }
}