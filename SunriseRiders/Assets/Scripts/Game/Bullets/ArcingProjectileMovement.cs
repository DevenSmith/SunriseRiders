using System;
using Devens;
using UnityEngine;

//modified version of https://youtu.be/OPDl2uVaN_Q?si=Qkm6RVra47HhaWmk

namespace Game.Bullets
{
    public class ArcingProjectileMovement : PausableMonoBehavior
    {
        private float moveSpeed;
        [SerializeField] private float trajectoryMaxRelativeHeight;
        [SerializeField] private float distanceToTargetToDestroyProjectile = 1f;
        [SerializeField] private FloatSO maxMoveSpeed;
        
        private Vector3 targetPosition;
        private Vector3 trajectoryStartPoint;
        private Vector3 trajectoryRange;
        private Vector3 projectileMoveDir;
        
        [SerializeField] private AnimationCurve trajectoryAnimationCurve;
        [SerializeField] private AnimationCurve axisCorrectionAnimationCurve;
        [SerializeField] private AnimationCurve projectileSpeedAnimationCurve;

        private float nextYTrajectoryPosition;
        private float nextXTrajectoryPosition;
        private float nextPositionYCorrectionAbsolute;
        private float nextPositionXCorrectionAbsolute;

        private bool initialized = false;
        
        public void Initialize(Vector3 targetPos)
        {
            targetPosition = targetPos;
            trajectoryStartPoint = transform.position;
            initialized = true;
        }

        private void OnDisable()
        {
            initialized = false;
        }

        private void Update()
        {
            if (!initialized || Paused)
                return;
            
            UpdatePosition();
            
            if (Vector3.Distance(transform.position, targetPosition) < distanceToTargetToDestroyProjectile) {
                gameObject.SetActive(false);
                ObjectPooler.Instance.PoolObject(gameObject);
            }

        }

        private void UpdatePosition()
        {
            trajectoryRange = targetPosition - trajectoryStartPoint;


            if(Mathf.Abs(trajectoryRange.normalized.x) < Mathf.Abs(trajectoryRange.normalized.y)) {
                // Projectile will be curved on the X axis


                if (trajectoryRange.y < 0) {
                    // Target is located under shooter
                    moveSpeed = -moveSpeed;
                }


                UpdatePositionWithXCurve();


            } else {
                // Projectile will be curved on the Y axis


                if (trajectoryRange.x < 0) {
                    // Target is located behind shooter
                    moveSpeed = -moveSpeed;
                }


                UpdatePositionWithYCurve();
            }

        }
        
        private void UpdatePositionWithXCurve() {


        float nextPositionY = transform.position.y + moveSpeed * Time.deltaTime;
        float nextPositionYNormalized = (nextPositionY - trajectoryStartPoint.y) / trajectoryRange.y;


        float nextPositionXNormalized = trajectoryAnimationCurve.Evaluate(nextPositionYNormalized);
        nextXTrajectoryPosition = nextPositionXNormalized * trajectoryMaxRelativeHeight;


        float nextPositionXCorrectionNormalized = axisCorrectionAnimationCurve.Evaluate(nextPositionYNormalized);
        nextPositionXCorrectionAbsolute = nextPositionXCorrectionNormalized * trajectoryRange.x;


        if(trajectoryRange.x > 0 && trajectoryRange.y > 0) {
            nextXTrajectoryPosition = -nextXTrajectoryPosition;
        }


        if (trajectoryRange.x < 0 && trajectoryRange.y < 0) {
            nextXTrajectoryPosition = -nextXTrajectoryPosition;
        }




        float nextPositionX = trajectoryStartPoint.x + nextXTrajectoryPosition + nextPositionXCorrectionAbsolute;


        Vector3 newPosition = new Vector3(nextPositionX, nextPositionY, 0);


        CalculateNextProjectileSpeed(nextPositionYNormalized);
        projectileMoveDir = newPosition - transform.position;


        transform.position = newPosition;
    }


    private void UpdatePositionWithYCurve() {


        float nextPositionX = transform.position.x + moveSpeed * Time.deltaTime;
        float nextPositionXNormalized = (nextPositionX - trajectoryStartPoint.x) / trajectoryRange.x;


        float nextPositionYNormalized = trajectoryAnimationCurve.Evaluate(nextPositionXNormalized);
        nextYTrajectoryPosition = nextPositionYNormalized * trajectoryMaxRelativeHeight;


        float nextPositionYCorrectionNormalized = axisCorrectionAnimationCurve.Evaluate(nextPositionXNormalized);
        nextPositionYCorrectionAbsolute = nextPositionYCorrectionNormalized * trajectoryRange.y;


        float nextPositionY = trajectoryStartPoint.y + nextYTrajectoryPosition + nextPositionYCorrectionAbsolute;


        Vector3 newPosition = new Vector3(nextPositionX, nextPositionY, 0);


        CalculateNextProjectileSpeed(nextPositionXNormalized);
        projectileMoveDir = newPosition - transform.position;


        transform.position = newPosition;
    }

    private void CalculateNextProjectileSpeed(float nextPositionXNormalized) {
        float nextMoveSpeedNormalized = projectileSpeedAnimationCurve.Evaluate(nextPositionXNormalized);


        moveSpeed = nextMoveSpeedNormalized * maxMoveSpeed.Value;
    }


    }
}
