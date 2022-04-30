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

        movement.OnFacingChanged += ChangeSprites;
    }

    private void OnDestroy()
    {
        if (movement != null)
        {
            movement.OnFacingChanged -= ChangeSprites;
        }
    }

    private void Update()
    {
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

    private void ChangeSprites()
    {
        playerBodyImage.flipX = !IsMovementFacingRight;
    }
}
