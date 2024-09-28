using Devens;
using UnityEngine;

namespace Game.PowerUps
{
    [CreateAssetMenu (menuName = "Game/HealthPowerUpTypeSO")]
    public class HealthPowerUpTypeSO : PowerUpTypeSO
    {
        [SerializeField] private IntSO healAmount;
        
        public override void ApplyPowerUp(PowerUp caller)
        {
            GameManager.PlayerReference.characterHealth.Heal(healAmount.Value);
        }
    }
}
