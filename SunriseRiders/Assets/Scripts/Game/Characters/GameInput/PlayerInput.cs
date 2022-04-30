using System;
using UnityEngine;
using static UnityEngine.Input;
namespace Game.Characters.GameInput
{
    public class PlayerInput : CharacterInput
    {
        [SerializeField] private Transform armTransform;

        [Header("Debug")] 
        [SerializeField] private bool debuggingArmDirection = false;
        [SerializeField] private Vector3 debugArmDirection;
        public Vector3 ArmDirection => _armDirection;
        private Vector3 _armDirection;
        private Camera _camera;
        private Vector3 _mousePosition;

        public void Awake()
        {
            _camera = Camera.main;
        }


        public void Update()
        {
            _movementVector.x = GetAxis("Horizontal");
            
            if (GetAxis("Jump") > 0)
            {
                jump = true;
            }

            shoot = GetAxis("Fire1") > 0;

            if (!debuggingArmDirection)
            {
                _mousePosition = _camera.ScreenToWorldPoint(mousePosition);
                _mousePosition.z = 0;
                _armDirection = (_mousePosition - armTransform.position).normalized;
            }
            else
            {
                _armDirection = debugArmDirection.normalized;
            }
        }
    }
}
