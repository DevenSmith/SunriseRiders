using System;
using System.Collections.Generic;
using Devens;
using Game.Characters.Movement;
using UnityEngine;

namespace Game.Characters.GameInput
{
    public class PlayerAimController : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private Movement.Movement playerMovement;
        [SerializeField] private FloatSO aimRotationAngle;
        private Facing aimFacing;
        private Facing previousFacing;
        
        [Serializable]
        public struct AimTransform
        {
            public Transform transform;
            public float originalRotation;
        }

        [SerializeField] private List<AimTransform> aimTransforms;
        
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

            for (int i = 0; i < aimTransforms.Count; i++)
            {
                var aimTransform = aimTransforms[i];
                aimTransform.originalRotation = aimTransform.transform.eulerAngles.x;
                aimTransforms[i] = aimTransform;
            }
        }

        public void Update()
        {
            UpdateAimFacing();

            if (aimFacing == previousFacing)
            {
                return;
            }

            foreach (var aimTransform in aimTransforms)
            {
                var currentAngles = aimTransform.transform.eulerAngles;
            
                switch (aimFacing)
                {
                    case Facing.Forward:
                        currentAngles.x = aimTransform.originalRotation;
                        break;
                    case Facing.ForwardUp:
                        currentAngles.x = aimTransform.originalRotation - aimRotationAngle.Value;
                        break;
                    case Facing.ForwardDown:
                        currentAngles.x = aimTransform.originalRotation + aimRotationAngle.Value;
                        break;
                    case Facing.Up:
                        currentAngles.x = aimTransform.originalRotation - 90.0f;
                        break;
                    case Facing.Down:
                        currentAngles.x = aimTransform.originalRotation + 90.0f;
                        break;
                    default:
                        Debug.LogError("PlayerAimController was assigned a facing it shouldn't have been: " + aimFacing);
                        break;
                }
            
                aimTransform.transform.eulerAngles = currentAngles;
            }
        }

        private void UpdateAimFacing()
        {
            previousFacing = aimFacing;
            
            if (playerMovement.MovementVector2.x == 0.0f)
            {
                if (playerInput.AimVector.y > 0.1f)
                {
                    aimFacing = Facing.Up;
                }
                else if (playerInput.AimVector.y < -0.1f)
                {
                    aimFacing = Facing.Down;
                }
                else
                {
                    aimFacing = Facing.Forward;
                }
            }
            else
            {
                if (playerInput.AimVector.y > 0.1f)
                {
                    aimFacing = Facing.ForwardUp;
                }
                else if (playerInput.AimVector.y < -0.1f)
                {
                    aimFacing = Facing.ForwardDown;
                }
                else
                {
                    aimFacing = Facing.Forward;
                }
            }
        }

        
    }
}
