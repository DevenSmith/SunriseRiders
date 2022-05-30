using Devens;
using UnityEngine;

namespace Game.Characters.Shooting
{
    public class ShotGunManShooting : GunManShooting
    {
        [SerializeField] protected IntSO bulletsToShoot;
        [SerializeField] protected FloatSO maxSpreadAngle;

        protected override void Shoot()
        {
            var originalAngle = bulletSpawnPoint.eulerAngles;

            for (var i = 0; i < bulletsToShoot.Value; i++)
            {
                var modifiedAngle = originalAngle;
                modifiedAngle.z += Random.Range(-maxSpreadAngle.Value, maxSpreadAngle.Value);
                bulletSpawnPoint.eulerAngles = modifiedAngle;
                base.Shoot();
            }

            bulletSpawnPoint.eulerAngles = originalAngle;
        }
    }
}
