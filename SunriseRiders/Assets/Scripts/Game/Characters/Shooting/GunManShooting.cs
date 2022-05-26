using System;
using Devens;
using UnityEngine;

namespace Game.Characters.Shooting
{
    public class GunManShooting : MonoBehaviour
    {
        [SerializeField] private FloatSO delayBetweenShots;
        [SerializeField] private StringSO bulletPrefabName;
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private FloatSO playerDistanceToStartShooting;
        
        [SerializeField] private float shotDelay = 0.0f;

        private Transform _enemyTransform;
        private Transform _playerTransform;
        
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
        
        private void Shoot()
        {
            var bullet = ObjectPooler.Instance.GetPooledObject(bulletPrefabName.Value);
            bullet.SetActive(true);
            bullet.transform.position = bulletSpawnPoint.position;
            bullet.transform.rotation = bulletSpawnPoint.rotation;
        }
    }
}
