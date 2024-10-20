using System.Collections.Generic;
using UnityEngine;

namespace Devens
{
    public class AnimationEventReceiver : MonoBehaviour
    {
        [SerializeField] private List<AnimationEvent> animationEvents = new();

        public void OnAnimationEventTriggered(string eventName)
        {
            var matchingEvent = animationEvents.Find(se => se.eventName == eventName);
            matchingEvent?.OnAnimationEvent?.Invoke();
        }
    }
}
