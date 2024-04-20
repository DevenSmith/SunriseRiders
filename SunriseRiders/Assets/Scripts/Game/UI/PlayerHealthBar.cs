using System;
using Devens;
using DG.Tweening;
using Game.Characters.GameInput;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class PlayerHealthBar : MonoBehaviour
    {
        [SerializeField] private Slider healthBarSlider;
        [SerializeField] private float maxDuration = 1.0f;
        
        private Health.Health _playerHealth;

        private void Start()
        {
           _playerHealth = FindObjectOfType<PlayerInput>().GetComponent<Health.Health>();
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
            var newValue = (float) _playerHealth.CurrentHealth / (float) _playerHealth.StartingHealth;
            var change = Mathf.Abs(healthBarSlider.value - newValue);
            DOTween.To(() => healthBarSlider.value, x => healthBarSlider.value = x, newValue, maxDuration * change);
        }
    }
}
