using System;
using System.Collections;
using Devens;
using Game.Characters.Movement;
using UnityEngine;

namespace Game.Characters.GameInput
{
    public class EnemyAimController : PausableMonoBehavior
    {
        [SerializeField] private Transform aimTransform;
        [SerializeField] private FloatSO aimRotationAngle;
        [SerializeField] private FloatSO aimFrequency;
        [SerializeField] private FloatSO aimHeightDifference;
        [SerializeField] private FloatSO aimNearnessDistance;
        
        private float _originalAimRotation;
        private Transform _playerTransform;
        private Transform _enemyTransform;
        
        private Facing _aimFacing;
        private Facing _previousFacing;

        [SerializeField] private float remainingDelay = 0.0f;

        private void Awake()
        {
            _enemyTransform = transform;
            _originalAimRotation = aimTransform.eulerAngles.x;
        }

        protected override void Start()
        {
            base.Start();
            _playerTransform = EnemyManager.Instance.PlayerTransform;
            remainingDelay = 0;
        }

        public void Update()
        {
            if (Paused)
            {
                return;
            }
            
            remainingDelay -= Time.deltaTime;
            
            if (remainingDelay > 0.0f)
            {
                return;
            }
            UpdateAimFacing();
            remainingDelay = aimFrequency.Value;

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
            
            if (_playerTransform.position.y < _enemyTransform.position.y - aimHeightDifference.Value)
            {
                _aimFacing = Facing.Down;
                if (Mathf.Abs(_playerTransform.position.x - _enemyTransform.position.x) > aimNearnessDistance.Value)
                {
                    _aimFacing = Facing.ForwardDown;
                }
            }
            else if (_playerTransform.position.y > _enemyTransform.position.y + aimHeightDifference.Value)
            {
                _aimFacing = Facing.Up;
                if (Mathf.Abs(_playerTransform.position.x - _enemyTransform.position.x) > aimNearnessDistance.Value)
                {
                    _aimFacing = Facing.ForwardUp;
                }
            }
            else
            {
                _aimFacing = Facing.Forward;
            }
        }
    }
}
