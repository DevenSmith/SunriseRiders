using System;
using Devens;
using Game.Damage;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Bullets
{
    public class BulletDamager : MonoBehaviour, IDamager
    {
        [SerializeField] private IntSO bulletDamage;
        [SerializeField] private UnityEvent onDamageDealt;
        
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
            damageable.TakeDamage(bulletDamage.Value);
            gameObject.SetActive(false);
            ObjectPooler.Instance.PoolObject(gameObject);
        }
    }
}
