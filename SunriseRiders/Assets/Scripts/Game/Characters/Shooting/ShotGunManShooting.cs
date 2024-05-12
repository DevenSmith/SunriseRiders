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
            var targetPos = _playerTargetTransform.position;
            var position = bulletSpawnPoint.position;
            targetPos.z = position.z;
            bulletSpawnPoint.right = (targetPos - position).normalized;
            var originalAngle = bulletSpawnPoint.eulerAngles;

            for (var i = 0; i < bulletsToShoot.Value; i++)
            {
                var modifiedAngle = originalAngle;
                modifiedAngle.z += Random.Range(-maxSpreadAngle.Value, maxSpreadAngle.Value);
                bulletSpawnPoint.eulerAngles = modifiedAngle;
                var bullet = ObjectPooler.Instance.GetPooledObject(bulletPrefabName.Value);
                bullet.SetActive(true);
                bullet.transform.position = position;
                bullet.transform.rotation = bulletSpawnPoint.rotation;
            }

            bulletSpawnPoint.eulerAngles = originalAngle;
            onShoot?.Invoke();
        }
    }
}
