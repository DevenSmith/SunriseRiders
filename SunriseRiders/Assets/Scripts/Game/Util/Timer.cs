using Devens;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Util
{
    public class Timer : PausableMonoBehavior
    {
        [SerializeField] private FloatSO timeLimit;
        
        private float remainingTime = 0.0f;

        public UnityEvent onTimeLimitReached;

        [SerializeField] private bool isRepeating = false;

        private void OnEnable()
        {
            remainingTime = timeLimit.Value;
        }


        void Update()
        {
            if (Paused || remainingTime < 0.0f)
                return;

            remainingTime -= Time.deltaTime;

            if (remainingTime < 0.0f)
            {
                onTimeLimitReached?.Invoke();

                if (isRepeating)
                {
                    remainingTime = timeLimit.Value;
                }
            }

        }
    }
}
