using UnityEngine;
using static UnityEngine.Input;
namespace Game.Characters.GameInput
{
    public class PlayerInput : CharacterInput
    {
        [SerializeField] private bool inputLocked = true;
        
        public void Update()
        {
            if (inputLocked)
            {
                return;
            }

            _movementVector.x = GetAxis("Horizontal");
            _aimVector.y = GetAxis("Vertical");
            if (GetAxis("Jump") > 0)
            {
                jump = true;
            }
            shoot = GetAxis("Fire1") > 0;
        }

        public void LockPlayerInput()
        {
            inputLocked = true;
        }

        public void UnlockPlayerInput()
        {
            inputLocked = false;
        }
    }
}
