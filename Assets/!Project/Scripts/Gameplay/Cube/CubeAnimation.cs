﻿using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Gameplay.Cube
{
    [RequireComponent(typeof(Animator))]
    public class CubeAnimation : MonoBehaviour
    {
        private static readonly int DestroyParam = Animator.StringToHash("Destroy");
        private static readonly int Idle = Animator.StringToHash("Idle");
        private const float IdleDelayMaxTime = 0.5f;
        private const float DeathTime = 1.6f;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            DelayIdle(this.GetCancellationTokenOnDestroy()).Forget();
        }

        public void PlayIdle()
        {
            _animator.SetTrigger(Idle);
        }

        public async UniTask PlayDeath(CancellationToken cancellationToken)
        {
            _animator.SetTrigger(DestroyParam);
            await UniTask.WaitForSeconds(DeathTime, cancellationToken: cancellationToken);
        }

        private async UniTaskVoid DelayIdle(CancellationToken cancellationToken)
        {
            await UniTask.WaitForSeconds(Random.Range(0, IdleDelayMaxTime), cancellationToken: cancellationToken);
            PlayIdle();
        }
    }
}