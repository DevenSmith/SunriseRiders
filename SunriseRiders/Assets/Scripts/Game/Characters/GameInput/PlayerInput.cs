using static UnityEngine.Input;
namespace Game.Characters.GameInput
{
    public class PlayerInput : CharacterInput
    {
        public void Update()
        {
            _movementVector.x = GetAxis("Horizontal");
            if (GetAxis("Jump") > 0)
            {
                jump = true;
            }
            
        }
    }
}
