using Devens;
using Game.UI;
using UnityEngine;

namespace Game.PowerUps
{
    [CreateAssetMenu (menuName = "Game/FireRatePowerUpTypeSO")]
    public class FireRatePowerUpTypeSO : PowerUpTypeSO
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private string id;
        [SerializeField] private FloatSO fireRateModifier;
        [SerializeField] private FloatSO modifierDuration;
        public override void ApplyPowerUp(PowerUp caller)
        {
            GameManager.PlayerReference.playerShooting.CurrentWeapon.modifiers.UpdateFireRateModifier(fireRateModifier.Value, modifierDuration.Value);
            
            UIIconWithTimerManager.ShowTimerUI(icon, modifierDuration.Value, id);
            
        }
    }
}
