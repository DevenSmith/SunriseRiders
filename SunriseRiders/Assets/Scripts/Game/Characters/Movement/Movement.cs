using System;
using System.Security.Cryptography.X509Certificates;
using Devens;
using Game.Characters.GameInput;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Characters.Movement
{
    public class Movement : PausableMonoBehavior
    {
        public Action OnFacingChanged;
        public Facing facing = Facing.Right;
        public bool isGrounded = false;
        public Collider[] currentGround;

        public bool IsCrouching => characterInput.crouch;
        public FloatSO crouchMovementModifier;
        public Vector2 MovementVector2 => _movementVelocity;

        [SerializeField] private FloatSO groundCheckRadius;
        [SerializeField] private Transform groundCheckPosition;
        [SerializeField] private FloatSO movementSpeed;
        
        [SerializeField] private CharacterInput characterInput;
        [SerializeField] private LayerMask whatIsGround;

        private Rigidbody _characterRb;
        private Vector2 _movementVelocity;

        private bool _pausedValuesSet;
        private Vector2 _prePausedVelocity;
        
        [Header("Jump Variables")]
        [SerializeField] private FloatSO jumpForce;
        public UnityEvent onJumped;
        [SerializeField] private SoundClipSO jumpSoundClipSO;
        
        [Header("Step Sound Variables")] 
        [SerializeField] private float stepInterval = 1.0f;
        [SerializeField] private SoundClipSO walkSoundClipSO;
        private float curStepInterval = 0.0f;

        [SerializeField]
        private float Modifier => modifierDurationRemaining > 0.0f ? modifier : 1.0f;

        private float modifier = 1.0f;
        private float modifierDurationRemaining = 0.0f;
        
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
            switch (Paused)
            {
                case true:
                {
                    if (!_pausedValuesSet)
                    {
                        _prePausedVelocity = _characterRb.velocity;
                        _pausedValuesSet = true;
                    }
                    _characterRb.velocity = Vector3.zero;
                    return;
                }
                case false when _pausedValuesSet:
                    _pausedValuesSet = false;
                    break;
            }

            currentGround = Physics.OverlapSphere(groundCheckPosition.position, groundCheckRadius.Value, whatIsGround);
            isGrounded = currentGround.Length > 0;
            
            _movementVelocity.x = characterInput.MovementVector.x * movementSpeed.Value * Modifier;
            if (IsCrouching)
            {
                _movementVelocity.x *= crouchMovementModifier.Value;
            }
            

            if (isGrounded && Mathf.Abs(_movementVelocity.x) > 0.0f)
            {
                curStepInterval -= Time.deltaTime;
                if (curStepInterval <= 0.0f)
                {
                    if (walkSoundClipSO != null)
                    {
                        SoundManger.Instance.PlaySound(walkSoundClipSO);
                    }

                    curStepInterval = stepInterval;
                }
            }
            
            _movementVelocity.y = _characterRb.velocity.y;

            if (characterInput.jump)
            {
                characterInput.jump = false;

                if (isGrounded)
                {
                    _movementVelocity.y = jumpForce.Value;

                    if (jumpSoundClipSO != null)
                    {
                        SoundManger.Instance.PlaySound(jumpSoundClipSO);
                    }

                    onJumped?.Invoke();
                }
            }
            
            _characterRb.velocity = _movementVelocity;
        }

        private void Update()
        {
            if (Paused)
                return;
            
            UpdateFacing();

            if (modifierDurationRemaining > 0.0f)
            {
                modifierDurationRemaining -= Time.deltaTime;
            }
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

        public void UpdateModifier(float modifier, float duration)
        {
            this.modifier = modifier;
            modifierDurationRemaining = duration;
        }
    }
}
