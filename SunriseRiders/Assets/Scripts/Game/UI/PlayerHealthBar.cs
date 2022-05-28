using System;
using Devens;
using Game.Characters.GameInput;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class PlayerHealthBar : MonoBehaviour
    {
        [SerializeField] private Slider healthBarSlider;
        
        private Characters.Health.Health _playerHealth;

        private void Start()
        {
           _playerHealth = FindObjectOfType<PlayerInput>().GetComponent<Characters.Health.Health>();
           _playerHealth.onHurt.AddListener(UpdateHealthBar);
           _playerHealth.onHeal.AddListener(UpdateHealthBar);
           _playerHealth.onDie.AddListener(UpdateHealthBar);
        }

        private void OnDestroy()
        {
            _playerHealth.onHurt.RemoveListener(UpdateHealthBar);
            _playerHealth.onHeal.RemoveListener(UpdateHealthBar);
            _playerHealth.onDie.RemoveListener(UpdateHealthBar);
        }

        private void UpdateHealthBar()
        {
            healthBarSlider.value = (float)_playerHealth.CurrentHealth / (float)_playerHealth.StartingHealth;
        }
    }
}
