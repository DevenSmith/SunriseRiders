using System;
using UnityEngine;
using static UnityEngine.Input;
namespace Game.Characters.GameInput
{
    public class PlayerInput : CharacterInput
    {
        public void Update()
        {
            _movementVector.x = GetAxis("Horizontal");
            _aimVector.y = GetAxis("Vertical");
            if (GetAxis("Jump") > 0)
            {
                jump = true;
            }
            shoot = GetAxis("Fire1") > 0;
        }
    }
}
