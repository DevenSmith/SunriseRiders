using System;
using Game.Characters.Movement.EnemyMovement;
using UnityEngine;

namespace Game.Characters.Movement
{
    public class PatrollingAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Facing facing = Facing.Right;
        [SerializeField] private Patrol patrol;
        
        private Transform _enemyTransform;
        private Transform _playerTransform;

        private void Awake()
        {
            patrol.OnPatrolStateChanged += OnPatrolStateChanged;
        }

        private void Start()
        {
            _enemyTransform = transform;
            _playerTransform = EnemyManager.Instance.PlayerTransform;
        }

        private void OnDestroy()
        {
            patrol.OnPatrolStateChanged -= OnPatrolStateChanged;
        }

        private void OnPatrolStateChanged(Patrol.PatrolStates patrolState)
        {
            if (patrolState == Patrol.PatrolStates.Idle)
            {
                animator.SetBool(MovementConstants.Idle, true);
                animator.SetBool(MovementConstants.Running, false);
            }
            else
            {
                animator.SetBool(MovementConstants.Idle, false);
                animator.SetBool(MovementConstants.Running, true);
            }
        }
        
        
        private void Update()
        {
            if (patrol.PatrolState == Patrol.PatrolStates.Idle)
            {
                
                if (_playerTransform.position.x < _enemyTransform.position.x && facing == Facing.Right)
                {
                    facing = Facing.Left;
                    _enemyTransform.eulerAngles += Vector3.up * 180;
                }

                if (_playerTransform.position.x > _enemyTransform.position.x && facing == Facing.Left)
                {
                    facing = Facing.Right;
                    _enemyTransform.eulerAngles += Vector3.up * 180;
                }
            }
            else
            {
                if (patrol.MovementVector.x > 0)
                {
                    if (facing == Facing.Left || facing == Facing.LeftUp || facing == Facing.LeftDown)
                    {
                        facing = Facing.Right;
                        _enemyTransform.eulerAngles += Vector3.up * 180;
                    }
                }
                if (patrol.MovementVector.x < 0)
                {
                    if (facing == Facing.Right || facing == Facing.RightUp || facing == Facing.RightDown)
                    {
                        facing = Facing.Left;
                        _enemyTransform.eulerAngles += Vector3.up * 180;
                    }
                }
                
            }
        }
        
    }
}
