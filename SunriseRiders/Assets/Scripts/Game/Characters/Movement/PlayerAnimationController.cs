using System;
using System.Collections;
using System.Collections.Generic;
using Game.Characters.Movement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Movement movement;
    [SerializeField] private SpriteRenderer playerBodyImage;
    [SerializeField] private Transform playerArmTransform;
    [SerializeField] private SpriteRenderer playerArmImage;
    [SerializeField] private SpriteRenderer playerGunImage;

    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerCharacterTransform;
    
    private bool IsMovementFacingRight =>
        movement.facing == Facing.Right || movement.facing == Facing.RightDown ||
        movement.facing == Facing.RightUp;

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
        animator.SetBool(MovementConstants.Running, movement.MovementVector2.x != 0.0f);

        if (movement.MovementVector2.y > 0.1f)
        {
            animator.SetBool(MovementConstants.Jumping, true);
            animator.SetBool(MovementConstants.Falling, false);
        }
        else if (movement.MovementVector2.y < -0.1f)
        {
            animator.SetBool(MovementConstants.Jumping, false);
            animator.SetBool(MovementConstants.Falling, true);
        }
        else
        {
            animator.SetBool(MovementConstants.Jumping, false);
            animator.SetBool(MovementConstants.Falling, false);
        }
        

        if (IsMovementFacingRight)
        {
            if (playerArmTransform.eulerAngles.z >= 270  || playerArmTransform.eulerAngles.z <= 90)
            {
                playerGunImage.flipY = false;
            }
            else
            {
                playerGunImage.flipY = true;
            }
        }
        else
        {
            if (playerArmTransform.eulerAngles.z >= 90 && playerArmTransform.eulerAngles.z <= 270)
            {
                playerGunImage.flipY = true;
            }
            else
            {
                playerGunImage.flipY = false;
            }
        }
    }

    private void OnChangedDirection()
    {
        playerCharacterTransform.eulerAngles += Vector3.up * 180;
        playerBodyImage.flipX = !IsMovementFacingRight;
    }
}
