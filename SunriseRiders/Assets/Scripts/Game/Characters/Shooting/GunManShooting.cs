﻿using System;
using System.IO.IsolatedStorage;
using Devens;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Game.Characters.Shooting
{
    public class GunManShooting : PausableMonoBehavior
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

        public UnityEvent onShoot;
        
        private bool CanStartShooting => Mathf.Abs((_enemyTransform.position - _playerTransform.position).magnitude) < playerDistanceToStartShooting.Value;

        protected override void Start()
        {
            _enemyTransform = transform;
            _playerTransform = EnemyManager.Instance.PlayerTransform;
            _playerTargetTransform = EnemyManager.Instance.PlayerTargetPoint;
            enemyHealth.onDie.AddListener(OnDeathAction);

            shotDelay = Random.Range(0.0f, delayBetweenShots.Value/2.0f);
        }

        private void OnDeathAction()
        {
            enabled = false;
            enemyHealth.onDie.RemoveListener(OnDeathAction);
        }


        private void Update()
        {
            if (Paused)
                return;
            
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
            
            onShoot?.Invoke();
        }
    }
}
