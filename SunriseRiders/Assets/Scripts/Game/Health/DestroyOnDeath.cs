using System;
using UnityEngine;

namespace Game.Health
{
    public class DestroyOnDeath : MonoBehaviour
    {
        [SerializeField] private Health health;

        private void Awake()
        {
            if (health == null)
            {
                Debug.LogError("Health Not Set on DestroyOnDeathComponent of " + name);
                return;
            }
            
            health.onDie.AddListener(OnDeathAction);
        }

        private void OnDeathAction()
        {
            gameObject.SetActive(false); 
            health.onDie.RemoveListener(OnDeathAction);
        }
    }
}
