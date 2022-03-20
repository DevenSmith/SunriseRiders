﻿using System;
using Devens;
using Game.Characters.GameInput;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Characters.Movement
{
    public class Movement : MonoBehaviour
    {
        public Facing facing = Facing.Right;
        public bool isGrounded = false;

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

        /// <summary>
        /// Will most likely move this somewhere else later since facing will be based on
        /// a combination of movement and arm position
        /// </summary>
        private void UpdateFacing()
        {
            if (characterInput.MovementVector.x == 0)
                return;

            if (characterInput.MovementVector.x > 0)
            {
                if (characterInput.MovementVector.y > 0)
                {
                    facing = Facing.RightUp;
                }
                else if(characterInput.MovementVector.y < 0)
                {
                    facing = Facing.RightDown;
                }
                else
                {
                    facing = Facing.Right;
                }
            }
            if (characterInput.MovementVector.x < 0)
            {
                if (characterInput.MovementVector.y > 0)
                {
                    facing = Facing.LeftUp;
                }
                else if(characterInput.MovementVector.y < 0)
                {
                    facing = Facing.LeftDown;
                }
                else
                {
                    facing = Facing.Left;
                }
            }
        }
    }
}