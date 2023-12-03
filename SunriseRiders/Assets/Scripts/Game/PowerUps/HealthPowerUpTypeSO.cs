using UnityEngine;

namespace Game.PowerUps
{
    [CreateAssetMenu (menuName = "Game/HealthPowerUpTypeSO")]
    public class HealthPowerUpTypeSO : PowerUpTypeSO
    {
        [SerializeField] private int healAmount = 100;
        
        public override void ApplyPowerUp(PowerUp caller)
        {
            GameManager.PlayerReference.characterHealth.Heal(healAmount);
        }
    }
}
