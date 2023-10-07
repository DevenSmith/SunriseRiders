using Game.Characters.GameInput;

namespace Game.Characters
{
    public class PlayerCharacterReference : CharacterReference
    {
        public PlayerInput playerInput;
        protected override void Initialize()
        {
            base.Initialize();

            if (playerInput == null)
            {
                playerInput = GetComponent<PlayerInput>();
            }
            
            GameManager.PlayerReference = this;
        }
    }
}
