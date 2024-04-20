using UnityEngine;
using UnityEngine.Events;

namespace Game.PowerUps
{
    public class PowerUp : MonoBehaviour
    {
        public UnityEvent onPowerUpApplied;
        [SerializeField] private PowerUpTypeSO powerUpType;

        private void OnTriggerEnter(Collider other)
        {
            if (other == GameManager.PlayerReference.characterCollider)
            {
                powerUpType.ApplyPowerUp(this);
                onPowerUpApplied?.Invoke();
            }
        }
    }
}
