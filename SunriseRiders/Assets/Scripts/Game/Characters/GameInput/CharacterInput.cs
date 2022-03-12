using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Characters.GameInput
{
    public class CharacterInput : MonoBehaviour
    {
        public bool jump = false;
        public bool shoot = false;
        public Vector2 MovementVector => _movementVector;
        protected Vector2 _movementVector;
    }
}
