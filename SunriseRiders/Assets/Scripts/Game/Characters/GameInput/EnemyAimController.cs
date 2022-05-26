using System;
using System.Collections;
using Devens;
using UnityEngine;

namespace Game.Characters.GameInput
{
    public class EnemyAimController : PausableMonoBehavior
    {
        [SerializeField] private Transform aimTransform;
        [SerializeField] private FloatSO aimRotationAngle;
        [SerializeField] private FloatSO aimFrequency;
        [SerializeField] private FloatSO aimHeightDifference;
        
        private float _originalAimRotation;
        private Transform _playerTransform;
        private Transform _enemyTransform;

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

            remainingDelay = aimFrequency.Value;
            
            if (_playerTransform.position.y < _enemyTransform.position.y - aimHeightDifference.Value
                && Math.Abs(aimTransform.eulerAngles.x - _originalAimRotation) < 0.5f)
            {
                aimTransform.eulerAngles += (Vector3.right * aimRotationAngle.Value);
            }
            else if (_playerTransform.position.y > _enemyTransform.position.y + aimHeightDifference.Value
                && Math.Abs(aimTransform.eulerAngles.x - _originalAimRotation) < 0.5f)
            {
                aimTransform.eulerAngles -= (Vector3.right * aimRotationAngle.Value);
            }
            else
            {
                var currentAngles = aimTransform.eulerAngles;
                currentAngles.x = _originalAimRotation;
                aimTransform.eulerAngles = currentAngles;
            }
        }
    }
}
