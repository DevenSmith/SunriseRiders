using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class PlayerTrigger : MonoBehaviour
    {
        public UnityEvent onTriggerEntered;
        public bool canOnlyTriggerOnce = true;
        
        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject != GameManager.PlayerReference.characterObject)
            {
                return;
            }
            
            onTriggerEntered?.Invoke();
            if (canOnlyTriggerOnce)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
