using System;
using Devens;
using UnityEngine;

namespace Game.Characters.GameInput
{
    /// <summary>
    /// for now it follows the mouse later consider having a second joystick to control arm direction.
    /// </summary>
    public class PlayerAimController : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private Transform aimTransform;
        [SerializeField] private FloatSO aimRotationAngle;
        private float _originalAimRotation;
        
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

            if (aimTransform == null)
                aimTransform = transform;

            _originalAimRotation = aimTransform.eulerAngles.x;
        }

        public void Update()
        {
            if (playerInput.AimVector.y > 0.1f && Math.Abs(aimTransform.eulerAngles.x - _originalAimRotation) < 0.5f)
            {
                aimTransform.eulerAngles -= (Vector3.right * aimRotationAngle.Value);
            }
            
            if (playerInput.AimVector.y < -0.1f && Math.Abs(aimTransform.eulerAngles.x - _originalAimRotation) < 0.5f)
            {
                aimTransform.eulerAngles += (Vector3.right * aimRotationAngle.Value);
            }

            if ((playerInput.AimVector.y != 0.0f ||
                 (!(Math.Abs(aimTransform.eulerAngles.x - _originalAimRotation) > 0.1f))) &&
                !(Math.Abs(aimTransform.eulerAngles.x - _originalAimRotation) < -0.1f))
            {
                return;
            }
            
            var currentAngles = aimTransform.eulerAngles;
            currentAngles.x = _originalAimRotation;
            aimTransform.eulerAngles = currentAngles;

        }
    }
}
