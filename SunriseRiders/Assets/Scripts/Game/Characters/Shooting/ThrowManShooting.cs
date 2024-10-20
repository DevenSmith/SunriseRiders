using JetBrains.Annotations;
using UnityEngine;

namespace Game.Characters.Shooting
{
    public class ThrowManShooting : GunManShooting
    {
        [SerializeField] private Animator animator;
        protected override void Shoot()
        {
            animator.SetTrigger("THROW");
        }

        [UsedImplicitly]
        public void SpawnProjectile()
        {
            onShoot?.Invoke();
            Debug.Log("Throw Projectile Spawned!");
        }
    }
}
