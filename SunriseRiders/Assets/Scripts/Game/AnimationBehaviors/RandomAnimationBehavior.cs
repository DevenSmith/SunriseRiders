using UnityEngine;

namespace Game.AnimationBehaviors
{
    public class RandomAnimationBehavior : StateMachineBehaviour
    {
        [SerializeField] private int animations = 5;
        [SerializeField] private string floatVariableName = "";
        

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var animation = Random.Range(0, animations);
            animator.SetFloat(floatVariableName, animation);
        }
    }
}
