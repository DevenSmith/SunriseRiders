using System.Runtime.CompilerServices;
using Devens;
using Game.Damage;
using Game.Health;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Characters.Health
{
    public class Health : MonoBehaviour, IDamageable, IHealth
    {
        [SerializeField] private IntSO characterStartingHealth;
        [SerializeField] private int characterCurrentHealth;

        [SerializeField] private UnityEvent onHurt;
        
        public int CurrentHealth => characterCurrentHealth;
        
        public void Awake()
        {
            characterCurrentHealth = characterStartingHealth.Value;
        }

        public void TakeDamage(int amount)
        {
            Hurt(amount);
        }

        public void Heal(int amount)
        {
            characterCurrentHealth = Mathf.Min(characterCurrentHealth + amount, characterStartingHealth.Value);
        }

        public void Hurt(int amount)
        {
            if (amount <= 0)
                return;
            characterCurrentHealth -= amount;
            if (characterCurrentHealth <= 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                onHurt.Invoke();
            }
        }
    }
}
