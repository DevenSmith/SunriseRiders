using UnityEngine;

namespace Game.PowerUps
{
    public class PowerUp : MonoBehaviour
    {
        [SerializeField] private PowerUpTypeSO powerUpType;

        private void OnTriggerEnter(Collider other)
        {
            if (other == GameManager.PlayerReference.characterCollider)
            {
                powerUpType.ApplyPowerUp(this);
            }
        }
    }
}
