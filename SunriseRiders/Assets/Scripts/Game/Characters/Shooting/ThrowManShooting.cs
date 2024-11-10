using System.Threading.Tasks;
using Devens;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Characters.Shooting
{
    public class ThrowManShooting : GunManShooting
    {
        [SerializeField] private Animator animator;
        [SerializeField] private StringSO projectilePrefabID;
        [SerializeField] private Transform spawnPoint;


        private Transform target;

        protected override void Start()
        {
            base.Start();
            target = GameManager.PlayerReference.transform;
        }

        protected override void Shoot()
        {
            animator.SetTrigger("THROW");
        }

        [UsedImplicitly]
        public void SpawnProjectile()
        {
            var projectile = ObjectPooler.Instance.GetPooledObject(projectilePrefabID.Value);
            projectile.gameObject.SetActive(true);
            projectile.transform.position = spawnPoint.position;
            LaunchProjectile(projectile);
            onShoot?.Invoke();
        }

        private async void LaunchProjectile(GameObject projectileObj)
        {
            var projectile = projectileObj.GetComponent<Rigidbody>();

            await Task.Yield();

            Vector3 startPosition = projectile.position;
            Vector3 targetPosition = target.position + Vector3.up;

            // Calculate the difference in position
            float heightDifference = targetPosition.y - startPosition.y;
            Vector3 horizontalDistanceVec =
                new Vector3(targetPosition.x - startPosition.x, 0, targetPosition.z - startPosition.z);
            float horizontalDistance = horizontalDistanceVec.magnitude;

            // Calculate the required velocity to reach the target
            Vector3 launchVelocity =
                CalculateLaunchVelocity(horizontalDistance, heightDifference, horizontalDistanceVec);

            if (launchVelocity == Vector3.zero)
            {
                Debug.LogWarning("Target is unreachable with current parameters.");
                ObjectPooler.Instance.PoolObject(projectileObj);
                return;
            }

            // Apply the calculated velocity to the projectile
            projectile.velocity = launchVelocity;
            Debug.Log($"Projectile launched with velocity: {launchVelocity}");
        }

        private Vector3 CalculateLaunchVelocity(float distance, float heightDifference, Vector3 direction)
        {
            // Get gravity value (use Unity's default gravity)
            float g = Mathf.Abs(Physics.gravity.y);

            // Calculate the necessary initial velocity
            float discriminant = g * (distance * distance) - 2 * g * heightDifference * distance;

            if (discriminant < 0)
            {
                // No valid solution exists if the discriminant is negative
                return Vector3.zero;
            }

            // Calculate the initial velocity using the quadratic formula
            float velocitySquared = (g * distance * distance) /
                                    (2 * (distance * Mathf.Tan(45f * Mathf.Deg2Rad) - heightDifference));

            if (velocitySquared <= 0)
            {
                return Vector3.zero;
            }

            float velocity = Mathf.Sqrt(velocitySquared);

            // Calculate the velocity components
            Vector3 velocityVector = direction.normalized * velocity;
            velocityVector.y = velocity * Mathf.Sin(45f * Mathf.Deg2Rad);

            return velocityVector;
        }
    }
}
