using System;
using System.Collections.Generic;
using Devens;
using Game.Damage;
using Game.Health.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Bullets
{
    public class BulletDamager : MonoBehaviour, IDamager
    {
        [SerializeField] private IntSO bulletDamage;
        [SerializeField] private UnityEvent onDamageDealt;

        [SerializeField] private List<DamageTypeSO> damageTypes;

        public void OnTriggerEnter(Collider other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                DealDamage(damageable);
            }
        }

        public void DealDamage(IDamageable damageable)
        {
            damageable.TakeDamage(bulletDamage.Value, damageTypes);
            gameObject.SetActive(false);
            ObjectPooler.Instance.PoolObject(gameObject);
        }
    }
}
