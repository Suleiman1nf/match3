using UnityEngine;

namespace _Project.Scripts.Utils
{
    public class RandomizeAnimationCycleOffset : StateMachineBehaviour
    {
        bool _hasRandomized;
 
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!_hasRandomized)
            {
                animator.Play(stateInfo.fullPathHash, layerIndex, Random.Range(-0f, 1f));
                _hasRandomized = true;
            }
        }
    }
}
