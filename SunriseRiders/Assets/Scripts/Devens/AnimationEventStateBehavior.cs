using UnityEngine;

//from https://youtu.be/XEDi7fUCQos?si=GYieU65COhWjNy0E
namespace Devens
{
    public class AnimationEventStateBehavior : StateMachineBehaviour
    {
        public string eventName;
        [Range(0f, 1f)] public float triggerTime;

        private bool hasTriggered;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            hasTriggered = false;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var currentTime = stateInfo.normalizedTime % 1f;
            if (!hasTriggered && currentTime >= triggerTime)
            {
                NotifyReciever(animator);
                hasTriggered = true;
            }
        }

        private void NotifyReciever(Animator animator)
        {
            var receiver = animator.GetComponent<AnimationEventReceiver>();
            if (receiver != null)
            {
                receiver.OnAnimationEventTriggered(eventName);
            }
            
        }
    }
}
