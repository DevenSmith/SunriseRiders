using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Characters.GameInput
{
    public class CharacterInput : PausableMonoBehavior
    {
        public bool jump = false;
        public bool crouch = false;
        public bool shoot = false;
        public Vector2 MovementVector => _movementVector;
        protected Vector2 _movementVector;
        public Vector2 AimVector => _aimVector;
        protected Vector2 _aimVector;
        
    }
}
