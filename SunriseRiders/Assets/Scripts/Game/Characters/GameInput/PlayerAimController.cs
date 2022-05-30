using System;
using System.Runtime.CompilerServices;
using Devens;
using Game.Characters.Movement;
using UnityEngine;

namespace Game.Characters.GameInput
{
    /// <summary>
    /// for now it follows the mouse later consider having a second joystick to control arm direction.
    /// </summary>
    public class PlayerAimController : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private Movement.Movement playerMovement;
        [SerializeField] private Transform aimTransform;
        [SerializeField] private FloatSO aimRotationAngle;
        private float _originalAimRotation;
        private Facing _aimFacing;
        private Facing _previousFacing;
        
        private bool IsFacingRight =>
            playerMovement.facing == Facing.Right
            || playerMovement.facing == Facing.RightDown
            || playerMovement.facing == Facing.RightUp;
        
        public void Start()
        {
            if (playerInput == null)
            {
                playerInput = GetComponent<PlayerInput>();
                if (playerInput == null)
                {
                    Debug.LogError(gameObject.name + " armController doesn't have a player input");
                }
            }

            if (playerMovement == null)
            {
                playerMovement = GetComponent<Movement.Movement>();
                if (playerMovement == null)
                {
                    Debug.LogError(gameObject.name + "playerMovement is null and couldn't find movement component");
                }
            }
            
            if (aimTransform == null)
                aimTransform = transform;

            _originalAimRotation = aimTransform.eulerAngles.x;
        }

        public void Update()
        {
            UpdateAimFacing();

            if (_aimFacing == _previousFacing)
            {
                return;
            }
            var currentAngles = aimTransform.eulerAngles;
            
            switch (_aimFacing)
            {
                case Facing.Forward:
                    currentAngles.x = _originalAimRotation;
                    break;
                case Facing.ForwardUp:
                    currentAngles.x = _originalAimRotation - aimRotationAngle.Value;
                    break;
                case Facing.ForwardDown:
                    currentAngles.x = _originalAimRotation + aimRotationAngle.Value;
                    break;
                case Facing.Up:
                    currentAngles.x = _originalAimRotation - 90.0f;
                    break;
                case Facing.Down:
                    currentAngles.x = _originalAimRotation + 90.0f;
                    break;
                default:
                    Debug.LogError("PlayerAimController was assigned a facing it shouldn't have been: " + _aimFacing);
                    break;
            }
            
            aimTransform.eulerAngles = currentAngles;
        }

        private void UpdateAimFacing()
        {
            _previousFacing = _aimFacing;
            
            if (playerMovement.MovementVector2.x == 0.0f)
            {
                if (playerInput.AimVector.y > 0.1f)
                {
                    _aimFacing = Facing.Up;
                }
                else if (playerInput.AimVector.y < -0.1f)
                {
                    _aimFacing = Facing.Down;
                }
                else
                {
                    _aimFacing = Facing.Forward;
                }
            }
            else
            {
                if (playerInput.AimVector.y > 0.1f)
                {
                    _aimFacing = Facing.ForwardUp;
                }
                else if (playerInput.AimVector.y < -0.1f)
                {
                    _aimFacing = Facing.ForwardDown;
                }
                else
                {
                    _aimFacing = Facing.Forward;
                }
            }
        }

        
    }
}
