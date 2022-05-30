using System;
using Devens;
using UnityEngine;

namespace Game.Characters.Shooting
{
    public class GunManShooting : MonoBehaviour
    {
        [SerializeField] protected FloatSO delayBetweenShots;
        [SerializeField] protected StringSO bulletPrefabName;
        [SerializeField] protected Transform bulletSpawnPoint;
        [SerializeField] protected FloatSO playerDistanceToStartShooting;
        
        [SerializeField] protected float shotDelay = 0.0f;

        protected Transform _enemyTransform;
        protected Transform _playerTransform;
        
        private bool CanStartShooting => Mathf.Abs((_enemyTransform.position - _playerTransform.position).magnitude) < playerDistanceToStartShooting.Value;

        private void Start()
        {
            _enemyTransform = transform;
            _playerTransform = EnemyManager.Instance.PlayerTransform;
        }


        private void Update()
        {
            if (shotDelay > 0.0f)
            {
                shotDelay -= Time.deltaTime;
            }
            
            if (!(shotDelay <= 0.0f))
            {
                return;
            }
            
            shotDelay = delayBetweenShots.Value;
            if (CanStartShooting)
            {
                Shoot();
            }
        }
        
        protected virtual void Shoot()
        {
            var bullet = ObjectPooler.Instance.GetPooledObject(bulletPrefabName.Value);
            bullet.SetActive(true);
            bullet.transform.position = bulletSpawnPoint.position;
            bullet.transform.rotation = bulletSpawnPoint.rotation;
        }
    }
}
