using System;
using System.Collections.Generic;
using Devens;
using Game.Damage;
using Game.Health;
using Game.Health.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Characters.Props
{
    public class PropHealth : MonoBehaviour, IDamageable, IHealth
    {
        [SerializeField] private IntSO propStartingHealth;
        [SerializeField] private int propCurrentHealth;

        [SerializeField] private UnityEvent onHurt;
 
        public int CurrentHealth => propCurrentHealth;

        public void Awake()
        {
            propCurrentHealth = propStartingHealth.Value;
        }

        public void TakeDamage(int amount, List<DamageTypeSO> damageTypes = null)
        {
            Hurt(amount);
        }

        public void Heal(int amount)
        {
            propCurrentHealth = Mathf.Min(propCurrentHealth + amount, propStartingHealth.Value);
        }

        public void Hurt(int amount)
        {
            if (amount <= 0)
                return;
            propCurrentHealth -= amount;
            if (propCurrentHealth <= 0)
            {
                
            }
            else
            {
                onHurt.Invoke();
            }
        }
    }
}
