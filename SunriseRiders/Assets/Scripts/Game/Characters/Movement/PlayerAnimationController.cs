using UnityEngine;

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
        [SerializeField] private Collider defaultCollider;
        [SerializeField] private Collider crouchedCollider;
        [SerializeField] private float verticalMovementAnimationTriggerBuffer = 0.25f;
        private bool IsMovementFacingRight =>
            movement.facing == Facing.Right || movement.facing == Facing.RightDown ||
            movement.facing == Facing.RightUp;

        private bool wasCrouching = false;
        
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
        }

        private void OnDestroy()
        {
            if (movement != null)
            {
                movement.OnFacingChanged -= OnChangedDirection;
            }
        }

        private void Update()
        {
            animator.SetBool(MovementConstants.Crouching, movement.IsCrouching);

            if (!wasCrouching && movement.IsCrouching)
            {
                weaponPivotTransform.position = crouchPivotHeightTransform.position;
                crouchedCollider.enabled = true;
                defaultCollider.enabled = false;
            }

            if (wasCrouching && !movement.IsCrouching)
            {
                weaponPivotTransform.position = defaultPivotHeightTransform.position;
                crouchedCollider.enabled = false;
                defaultCollider.enabled = true;
            }

            animator.SetBool(MovementConstants.Running, movement.MovementVector2.x != 0.0f);

            if (movement.MovementVector2.y > verticalMovementAnimationTriggerBuffer)
            {
                animator.SetBool(MovementConstants.Jumping, true);
                animator.SetBool(MovementConstants.Falling, false);
            }
            else if (movement.MovementVector2.y < - verticalMovementAnimationTriggerBuffer)
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

        private void OnChangedDirection()
        {
            playerCharacterTransform.eulerAngles += Vector3.up * 180;
        }
    }
}
