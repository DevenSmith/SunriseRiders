using System;
using System.Collections;
using System.Collections.Generic;
using Game.Characters.Movement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Movement movement;

    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerCharacterTransform;
    [SerializeField] private float verticalMovementAnimationTriggerBuffer = 0.25f;
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
    }

    private void OnChangedDirection()
    {
        playerCharacterTransform.eulerAngles += Vector3.up * 180;
    }
}
