using Devens;
using Game.UI;
using UnityEngine;

namespace Game.PowerUps
{
    [CreateAssetMenu (menuName = "Game/SpeedPowerUpTypeSO")]
    public class SpeedPowerUpTypeSO : PowerUpTypeSO
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private string id;
        [SerializeField] private FloatSO speedModifier;
        [SerializeField] private FloatSO modifierDuration;
        public override void ApplyPowerUp(PowerUp caller)
        { 
            GameManager.PlayerReference.playerMovement.UpdateModifier(speedModifier.Value, modifierDuration.Value);
            
            UIIconWithTimerManager.ShowTimerUI(icon, modifierDuration.Value, id);
        }
    }
}
