using System;
using System.IO.IsolatedStorage;
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
        [SerializeField] protected Health.Health enemyHealth;

        protected Transform _enemyTransform;
        protected Transform _playerTransform;
        protected Transform _playerTargetTransform;
        
        private bool CanStartShooting => Mathf.Abs((_enemyTransform.position - _playerTransform.position).magnitude) < playerDistanceToStartShooting.Value;

        private void Start()
        {
            _enemyTransform = transform;
            _playerTransform = EnemyManager.Instance.PlayerTransform;
            _playerTargetTransform = EnemyManager.Instance.PlayerTargetPoint;
            enemyHealth.onDie.AddListener(OnDeathAction);

        }

        private void OnDeathAction()
        {
            enabled = false;
            enemyHealth.onDie.RemoveListener(OnDeathAction);
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
            var targetPos = _playerTargetTransform.position;
            var position = bulletSpawnPoint.position;
            targetPos.z = position.z;
            bulletSpawnPoint.right = (targetPos - position).normalized;
            var bullet = ObjectPooler.Instance.GetPooledObject(bulletPrefabName.Value);
            bullet.SetActive(true);
            bullet.transform.position = position;
            bullet.transform.rotation = bulletSpawnPoint.rotation;
        }
    }
}
