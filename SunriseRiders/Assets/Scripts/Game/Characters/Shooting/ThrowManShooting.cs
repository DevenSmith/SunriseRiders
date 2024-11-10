using Devens;
using Game.Bullets;
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
            var arcMovement = projectile.GetComponent<ArcingProjectileMovement>();
            arcMovement.Initialize(GameManager.PlayerReference.transform.position + Vector3.up);
            onShoot?.Invoke();
        }
    }
}
