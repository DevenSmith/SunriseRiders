using Devens;
using UnityEngine;

namespace Game.Characters.Shooting.WeaponShooting
{
    public class ShotgunShooting : Weapons_Shooting.WeaponShooting
    {
        [SerializeField] private float maxSpreadAngle = 15.0f;
        [SerializeField] private int bulletsPerShot = 5;
        
        private float spawnPointOriginalRotation;

        private void Start()
        {
            spawnPointOriginalRotation = bulletSpawnPoint.eulerAngles.x;
        }

        public override void Shoot()
        {
            if (shotDelay > 0.0f)
                return;

            for (int i = 0; i < bulletsPerShot; i++)
            {
                var rotation = spawnPointOriginalRotation + Random.Range(-maxSpreadAngle, maxSpreadAngle);
                var eulerAngles = bulletSpawnPoint.eulerAngles;
                eulerAngles.x = rotation;
                bulletSpawnPoint.eulerAngles = eulerAngles;
                
                var bullet = ObjectPooler.Instance.GetPooledObject(bulletPrefabName.Value);
                bullet.SetActive(true);
                bullet.transform.position = bulletSpawnPoint.position;
                bullet.transform.rotation = bulletSpawnPoint.rotation;
            }
            shotDelay = delayBetweenShots.Value;

        }
    }
}
