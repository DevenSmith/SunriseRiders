using System.Collections;
using System.Linq;
using Devens;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Events;

namespace Game.Characters.Movement
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Movement movement;

        [SerializeField] private Animator animator;
        [SerializeField] private Transform playerCharacterTransform;
        [SerializeField] private Transform weaponPivotTransform;
        [SerializeField] private Transform defaultPivotHeightTransform;
        [SerializeField] private Transform crouchPivotHeightTransform;
        [SerializeField] private CapsuleCollider playerCollider;
        [SerializeField] private float defaultColliderCenter;
        [SerializeField] private float defaultColliderHeight;
        [SerializeField] private float crouchColliderCenter;
        [SerializeField] private float crouchColliderHeight;
        [SerializeField] private float verticalMovementAnimationTriggerBuffer = 0.25f;
        private bool IsMovementFacingRight =>
            movement.facing == Facing.Right || movement.facing == Facing.RightDown ||
            movement.facing == Facing.RightUp;

        private bool wasCrouching = false;
        
        [SerializeField] private Rig rig;
        [SerializeField] private RigBuilder rigBuilder;
        [SerializeField] private TwoBoneIKConstraint[] boneIKConstraints;
        [SerializeField] private Health.Health health;
        [SerializeField] private GameObject gun;
        private const string DieTrigger = "DIE";

        private bool dying = false;
        private bool doneDying = false;
        [SerializeField] private Rigidbody charRigidbody;
        public UnityEvent onDeathFinished;

        [SerializeField] private TagSO runningGroundTag;

        private bool ShouldRun
        {
            get
            {
                if (movement.MovementVector2.x != 0.0f
                    || movement.isGrounded && IsOnRunningGround)
                {
                    return true;
                }
                
                return false;
            }
        }

        private bool IsOnRunningGround
        {
            get
            {
                var runningGrounds = TagUtility.FindGameObjectsWithTag(runningGroundTag);
                
                foreach (var collider in movement.currentGround)
                {
                    if (runningGrounds.Contains(collider.gameObject))
                    {
                        return true;
                    }
                }

                return false;
            }
        }
        
        private void Awake()
        {
            if (movement == null)
            {
                movement = GetComponent<Movement>();
            }
        
            if (movement == null)
            {
                Debug.LogError("Movement not set for " + name);
                return;
            }

            movement.OnFacingChanged += OnChangedDirection;
            
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

        private void OnDestroy()
        {
            if (movement != null)
            {
                movement.OnFacingChanged -= OnChangedDirection;
            }
            
            if (health != null)
            {
                health.onDie.RemoveListener(Died);
            }
        }

        private void Update()
        {
            if (dying)
            {
                if (!doneDying && animator.GetCurrentAnimatorStateInfo(0).IsName("Done"))
                {
                    doneDying = true;
                    onDeathFinished?.Invoke();
                }
            }
            else
            {
                animator.SetBool(MovementConstants.Crouching, movement.IsCrouching);

                if (!wasCrouching && movement.IsCrouching)
                {
                    weaponPivotTransform.position = crouchPivotHeightTransform.position;
                    var center = playerCollider.center;
                    center.y = crouchColliderCenter;
                    playerCollider.center = center;
                    playerCollider.height = crouchColliderHeight;
                }

                if (wasCrouching && !movement.IsCrouching)
                {
                    weaponPivotTransform.position = defaultPivotHeightTransform.position;
                    var center = playerCollider.center;
                    center.y = defaultColliderCenter;
                    playerCollider.center = center;
                    playerCollider.height = defaultColliderHeight;
                }
                
                
                
                animator.SetBool(MovementConstants.Running, ShouldRun);

                if (!movement.isGrounded && movement.MovementVector2.y > verticalMovementAnimationTriggerBuffer)
                {
                    animator.SetBool(MovementConstants.Jumping, true);
                    animator.SetBool(MovementConstants.Falling, false);
                }
                else if (!movement.isGrounded && movement.MovementVector2.y < -verticalMovementAnimationTriggerBuffer)
                {
                    animator.SetBool(MovementConstants.Jumping, false);
                    animator.SetBool(MovementConstants.Falling, true);
                }
                else
                {
                    animator.SetBool(MovementConstants.Jumping, false);
                    animator.SetBool(MovementConstants.Falling, false);
                }

                wasCrouching = movement.IsCrouching;
            }
        }

        private void OnChangedDirection()
        {
            playerCharacterTransform.eulerAngles += Vector3.up * 180;
        }
        
        private void Died()
        {
            dying = true;
            gun.SetActive(false);

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
    }
}
