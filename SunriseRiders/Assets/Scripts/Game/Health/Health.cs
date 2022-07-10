using System.Collections.Generic;
using Devens;
using Game.Damage;
using Game.Health.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Health
{
    public class Health : MonoBehaviour, IDamageable, IHealth
    {
        [SerializeField] private bool invincible = false;
        
        [SerializeField] private IntSO characterStartingHealth;
        [SerializeField] private int characterCurrentHealth;

        public List<DamageTypeSO> immuneToDamageTypes = new List<DamageTypeSO>();

        public UnityEvent onDie;
        public UnityEvent onHurt;
        public UnityEvent onHeal;

        public int CurrentHealth => characterCurrentHealth;
        public int StartingHealth => characterStartingHealth.Value;
        
        public void Awake()
        {
            characterCurrentHealth = characterStartingHealth.Value;
        }

        public void TakeDamage(int amount, List<DamageTypeSO> damageTypes = null)
        {
            if (damageTypes != null && damageTypes.Count > 0)
            {
                foreach (var immuneType in immuneToDamageTypes)
                {
                    if (damageTypes.Contains(immuneType))
                    {
                        return;
                    }
                }
            }
            
            Hurt(amount);
        }

        public void Heal(int amount)
        {
            if (amount <= 0)
                return;

            characterCurrentHealth = Mathf.Min(characterCurrentHealth + amount, characterStartingHealth.Value);
            onHeal?.Invoke();
        }

        public void Hurt(int amount)
        {
            if (invincible || amount <= 0 || characterCurrentHealth <= 0)
                return;
            characterCurrentHealth -= amount;
            if (characterCurrentHealth <= 0)
            {
                onDie?.Invoke();
            }
            else
            {
                onHurt?.Invoke();
            }
        }
    }
}
