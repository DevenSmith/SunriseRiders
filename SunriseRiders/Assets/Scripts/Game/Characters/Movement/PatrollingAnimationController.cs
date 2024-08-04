using System.Collections;
using Game.Characters.Movement.EnemyMovement;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Game.Characters.Movement
{
    public class PatrollingAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Facing facing = Facing.Right;
        [SerializeField] private Patrol patrol;
        
        private Transform _enemyTransform;
        private Transform _playerTransform;

        [SerializeField] private Rig rig;
        [SerializeField] private RigBuilder rigBuilder;
        [SerializeField] private TwoBoneIKConstraint[] boneIKConstraints;
        [SerializeField] private Health.Health health;
        [SerializeField] private GameObject gun;
        private const string DieTrigger = "DIE";
        
        private bool dying = false;
        private bool doneDying = false;
        [SerializeField] private Collider[] colliders;
        [SerializeField] private Rigidbody charRigidbody;
        
        private void Awake()
        {
            patrol.OnPatrolStateChanged += OnPatrolStateChanged;
            
            if (health == null)
            {
                health = GetComponent<Health.Health>();
                if (health == null)
                {
                    Debug.LogError( gameObject.name+ " No Health Component Assigned to " + this.name);
                }
            }
            
            if (health != null)
            {
                health.onDie.AddListener(Died);
            }
        }

        private void Start()
        {
            _enemyTransform = transform;
            _playerTransform = EnemyManager.Instance.PlayerTransform;
        }

        private void OnDestroy()
        {
            patrol.OnPatrolStateChanged -= OnPatrolStateChanged;
            
            if (health != null)
            {
                health.onDie.RemoveListener(Died);
            }
        }
        
        private void Died()
        {
            patrol.OnPatrolStateChanged -= OnPatrolStateChanged;
            animator.SetBool(MovementConstants.Running, false);
            
            dying = true;
            gun.SetActive(false);

            foreach (var collider in colliders)
            {
                collider.enabled = false;
            }

            charRigidbody.isKinematic = true;
            charRigidbody.useGravity = false;

            StartCoroutine(SetRigWeight());

            
            var index = Random.Range(1, 4);
            animator.SetTrigger(DieTrigger + index.ToString());
        }

        private IEnumerator SetRigWeight()
        {
            while (animator != null && animator.IsInTransition(0))
            {
                yield return null;
            }
            yield return new WaitForEndOfFrame();
            
            foreach (var ikConstraint in boneIKConstraints)
            {
                ikConstraint.weight = 0.0f;
            }
            rig.weight = 0.0f;
            rigBuilder.Build();
        }

        private void OnPatrolStateChanged(Patrol.PatrolStates patrolState)
        {
            if (patrolState == Patrol.PatrolStates.Idle)
            {
                animator.SetBool(MovementConstants.Running, false);
            }
            else
            {
                animator.SetBool(MovementConstants.Running, true);
            }
        }
        
        
        private void Update()
        {
            if (dying)
            {
                if (!doneDying && animator.GetCurrentAnimatorStateInfo(0).IsName("Done"))
                {
                    doneDying = true;
                    gameObject.SetActive(false);
                }
            }
            else
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
}
