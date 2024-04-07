using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Cube
{
    public class CubeMovement : MonoBehaviour
    {
        private const float MoveDuration = 0.5f;
        private const Ease MoveEase = Ease.OutBack;

        public async UniTaskVoid MoveAsync(Vector3 destination, CancellationToken cancellationToken)
        {
            await transform.DOMove(destination, MoveDuration).SetEase(MoveEase).WithCancellation(cancellationToken);
        }
    }
}