using Devens;
using UnityEngine;

namespace Game.PowerUps
{
    [CreateAssetMenu (menuName = "Game/WeaponPowerUpTypeSO")]
    public class WeaponPowerUpTypeSO : PowerUpTypeSO
    {
        public StringSO weaponName;
        public override void ApplyPowerUp(PowerUp caller)
        {
            var weaponIndex = GameManager.PlayerReference.weaponSwapper.GetIndexOfWeapon(weaponName);

            if (weaponIndex < 0)
            {
                return;
            }
            GameManager.PlayerReference.weaponSwapper.SetWeapon(weaponIndex);
        }
    }
}
