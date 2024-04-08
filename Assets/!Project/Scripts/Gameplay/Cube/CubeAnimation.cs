using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Cube
{
    [RequireComponent(typeof(Animator))]
    public class CubeAnimation : MonoBehaviour
    {
        private static readonly int DestroyParam = Animator.StringToHash("Destroy");
        private static readonly int Idle = Animator.StringToHash("Idle");
        private Animator _animator;

        private bool _isDeadEventReceived;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayIdle()
        {
            _animator.SetTrigger(Idle);
        }

        public async UniTask PlayDeath(CancellationToken cancellationToken)
        {
            _isDeadEventReceived = false;
            _animator.SetTrigger(DestroyParam);
            await UniTask.WaitWhile(()=>!_isDeadEventReceived, cancellationToken: cancellationToken);
        }

        public void OnDeathEvent()
        {
            _isDeadEventReceived = true;
        }
    }
}