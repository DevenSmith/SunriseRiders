using System;
using Devens;
using Game.Characters.GameInput;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Characters.Movement
{
    public class Movement : MonoBehaviour
    {
        public Action OnFacingChanged;
        public Facing facing = Facing.Right;
        public bool isGrounded = false;
        public Vector2 MovementVector2 => _movementVelocity;

        [SerializeField] private FloatSO groundCheckRadius;
        [SerializeField] private Transform groundCheckPosition;
        [SerializeField] private FloatSO movementSpeed;
        [SerializeField] private FloatSO jumpForce;
        [SerializeField] private CharacterInput characterInput;
        [SerializeField] private LayerMask whatIsGround;
        
        private Rigidbody _characterRb;
        private Vector2 _movementVelocity;

        private void Awake()
        {
            if (_characterRb != null) return;
            
            _characterRb = GetComponentInChildren<Rigidbody>();
            if (_characterRb == null)
            {
                Debug.LogError(gameObject.name + "Doesn't have a RigidBody2D for their Movement component");
            }
        }
        
        private void FixedUpdate()
        {
            isGrounded = Physics.OverlapSphere(groundCheckPosition.position, groundCheckRadius.Value, whatIsGround).Length > 0;
            
            _movementVelocity.x = characterInput.MovementVector.x * movementSpeed.Value;
            _movementVelocity.y = _characterRb.velocity.y;

            if (characterInput.jump)
            {
                characterInput.jump = false;

                if (isGrounded)
                {
                    _movementVelocity.y = jumpForce.Value;
                }
            }
            
            _characterRb.velocity = _movementVelocity;
        }

        private void Update()
        {
            UpdateFacing();
        }

        /// <summary>
        /// Will most likely move this somewhere else later since facing will be based on
        /// a combination of movement and arm position
        /// </summary>
        private void UpdateFacing()
        {
            if (characterInput.MovementVector.x == 0)
                return;

            var newFacing = facing;
            if (characterInput.MovementVector.x > 0)
            {
                if (characterInput.MovementVector.y > 0)
                {
                    newFacing = Facing.RightUp;
                }
                else if(characterInput.MovementVector.y < 0)
                {
                    newFacing = Facing.RightDown;
                }
                else
                {
                    newFacing = Facing.Right;
                }
            }
            if (characterInput.MovementVector.x < 0)
            {
                if (characterInput.MovementVector.y > 0)
                {
                    newFacing = Facing.LeftUp;
                }
                else if(characterInput.MovementVector.y < 0)
                {
                    newFacing = Facing.LeftDown;
                }
                else
                {
                    newFacing = Facing.Left;
                }
            }

            if (newFacing != facing && OnFacingChanged != null)
            {
                facing = newFacing;
                OnFacingChanged.Invoke();
            }
        }
    }
}
