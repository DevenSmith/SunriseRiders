using Devens;
using UnityEngine;

namespace Game.Characters.Shooting.Weapons_Shooting
{
    public class ShotgunShooting : Weapons_Shooting.WeaponShooting
    {
        [SerializeField] private float maxSpreadAngle = 15.0f;
        [SerializeField] private int bulletsPerShot = 5;

        public override void Shoot()
        {
            if (shotDelay > 0.0f)
                return;

            for (int i = 0; i < bulletsPerShot; i++)
            {
                var rotation = bulletSpawnPoint.eulerAngles.z + Random.Range(-maxSpreadAngle, maxSpreadAngle);
                var eulerAngles = bulletSpawnPoint.eulerAngles;
                eulerAngles.z = rotation;
                
                var bullet = ObjectPooler.Instance.GetPooledObject(bulletPrefabName.Value);
                bullet.SetActive(true);
                bullet.transform.position = bulletSpawnPoint.position;
                bullet.transform.eulerAngles = eulerAngles;
            }
            shotDelay = delayBetweenShots.Value;

        }
    }
}
