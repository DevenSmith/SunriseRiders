using System;
using UnityEngine.Events;

namespace Devens
{
    [Serializable]
    public class AnimationEvent
    {
        public string eventName;
        public UnityEvent OnAnimationEvent;
    }
}
