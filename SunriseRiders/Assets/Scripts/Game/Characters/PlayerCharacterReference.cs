using Game.Characters.GameInput;

namespace Game.Characters
{
    public class PlayerCharacterReference : CharacterReference
    {
        public PlayerInput playerInput;
        public WeaponSwapper weaponSwapper;
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
            
            GameManager.PlayerReference = this;
        }
    }
}
