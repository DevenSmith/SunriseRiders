using Devens;
using UnityEngine;

namespace Game.PowerUps
{
    [CreateAssetMenu (menuName = "Game/FireRatePowerUpTypeSO")]
    public class FireRatePowerUpTypeSO : PowerUpTypeSO
    {
        [SerializeField] private FloatSO fireRateModifier;
        [SerializeField] private FloatSO modifierDuration;
        public override void ApplyPowerUp(PowerUp caller)
        {
            base.ApplyPowerUp(caller);
            GameManager.PlayerReference.playerShooting.CurrentWeapon.modifiers.UpdateFireRateModifier(fireRateModifier.Value, modifierDuration.Value);
        }
    }
}
