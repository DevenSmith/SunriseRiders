using System;
using System.Runtime.CompilerServices;
using Devens;
using Game.Characters.GameInput;
using Unity.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Characters.Movement.EnemyMovement
{
    /// <summary>
    /// currently only meant for flat ground
    /// will have to expand for jumping/falling.
    /// </summary>
    public class Patrol : CharacterInput
    {
        public enum PatrolTypes
        {
            PingPongInOrder,
            LoopingInOrder,
            RandomCantDouble,
            Random
        }

        public enum PatrolStates
        {
            Idle,
            Traveling
        }
        
        [SerializeField] private PatrolTypes patrolType;
        [SerializeField] private Transform[] patrolPoints;

        [SerializeField, ReadOnly] private PatrolStates patrolState;

        [SerializeField] private int patrolDestinationIndex;
        [SerializeField, ReadOnly] private int indexDirection = 1; 
        [SerializeField] private FloatSO bufferDistanceToStop;
        [SerializeField] private Movement movement;
        [SerializeField] private FloatSO timeToWaitAtPatrolPoint;

        public Action OnPatrolPointReached;
        
        public PatrolStates PatrolState => patrolState;

        public Action<PatrolStates> OnPatrolStateChanged;

        [SerializeField] private float idleTime = 0.0f;

        private Transform _myTransform;

        private void Awake()
        {
            _myTransform = transform;
        }

        public void Update()
        {
            if (Paused)
            {
                return;
            }

            if (patrolState == PatrolStates.Idle)
            {
                idleTime -= Time.deltaTime;

                if (idleTime < 0.0f)
                {
                    patrolState = PatrolStates.Traveling;
                    OnPatrolStateChanged?.Invoke(patrolState);
                }
            }
            else
            {
                if (Mathf.Abs(_myTransform.position.x - patrolPoints[patrolDestinationIndex].position.x) <
                    bufferDistanceToStop.Value)
                {
                    _movementVector.x = 0;
                    patrolState = PatrolStates.Idle;
                    idleTime = timeToWaitAtPatrolPoint.Value;
                    GetNextPatrolPoint();
                    OnPatrolPointReached?.Invoke();
                    OnPatrolStateChanged?.Invoke(patrolState);
                    return;
                }
                
                _movementVector.x = patrolPoints[patrolDestinationIndex].position.x < _myTransform.position.x ? -1.0f: 1.0f;
            }
        }

        private void GetNextPatrolPoint()
        {
            switch (patrolType)
            {
                case PatrolTypes.LoopingInOrder:
                {
                    patrolDestinationIndex++;
                    if (patrolDestinationIndex >= patrolPoints.Length)
                    {
                        patrolDestinationIndex = 0;
                    }

                    break;
                }
                case PatrolTypes.PingPongInOrder:
                {
                    if (indexDirection > 0)
                    {
                        patrolDestinationIndex++;
                        if (patrolDestinationIndex >= patrolPoints.Length -1)
                        {
                            indexDirection *= -1;
                        }
                    }
                    else
                    {
                        patrolDestinationIndex--;
                        if (patrolDestinationIndex <= 0)
                        {
                            indexDirection *= -1;
                        }
                    }

                    break;
                }
                case  PatrolTypes.RandomCantDouble:
                {
                    var newIndex = Random.Range(0, patrolPoints.Length);
                    var maxAttempts = 100;
                    
                    while (maxAttempts > 0 && newIndex == patrolDestinationIndex)
                    {
                        newIndex = Random.Range(0, patrolPoints.Length);
                        maxAttempts--;
                    }

                    if (maxAttempts == 0)
                    {
                        Debug.LogWarning(name + "Took max attempts to get a random patrol position!");
                    }
                    
                    patrolDestinationIndex = newIndex;
                    break;
                }
                case PatrolTypes.Random:
                {
                    patrolDestinationIndex = Random.Range(0, patrolPoints.Length);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
