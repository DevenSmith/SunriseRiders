using Game.Characters.GameInput;
using Game.Characters.Shooting;

namespace Game.Characters
{
    public class PlayerCharacterReference : CharacterReference
    {
        public PlayerInput playerInput;
        public WeaponSwapper weaponSwapper;
        public PlayerShooting playerShooting;
        public Movement.Movement playerMovement;
        
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

            if (playerMovement == null)
            {
                playerMovement = GetComponent<Movement.Movement>();

                if (playerMovement == null)
                {
                    playerMovement = GetComponentInChildren<Movement.Movement>();
                }
                
            }
            
            GameManager.PlayerReference = this;
        }
    }
}
