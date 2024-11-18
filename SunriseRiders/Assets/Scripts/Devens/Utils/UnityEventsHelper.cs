using UnityEngine;
using UnityEngine.Events;

namespace Devens.Utils
{
    public class UnityEventsHelper : MonoBehaviour
    {
        public UnityEvent onEnabled;
        public UnityEvent onDisabled;
        public void OnEnable()
        {
            onEnabled?.Invoke();
        }

        public void OnDisable()
        {
            onDisabled?.Invoke();
        }
    }
}
