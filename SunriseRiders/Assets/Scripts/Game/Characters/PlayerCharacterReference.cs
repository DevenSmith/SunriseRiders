using Game.Characters.GameInput;
using Game.Characters.Shooting;

namespace Game.Characters
{
    public class PlayerCharacterReference : CharacterReference
    {
        public PlayerInput playerInput;
        public WeaponSwapper weaponSwapper;
        public PlayerShooting playerShooting;
        protected override void Initialize()
        {
            base.Initialize();

            if (playerInput == null)
            {
                playerInput = GetComponent<PlayerInput>();
            }

            if (weaponSwapper == null)
            {
                weaponSwapper = GetComponent<WeaponSwapper>();
            }

            if (playerShooting == null)
            {
                playerShooting = GetComponent<PlayerShooting>();

                if (playerShooting == null)
                {
                    playerShooting = GetComponentInChildren<PlayerShooting>();
                }
            }
            
            GameManager.PlayerReference = this;
        }
    }
}
