using System;
using Devens;
using Game.Health;
using UnityEngine;

namespace Game.Characters.Enemies
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        [SerializeField] private StringSO healthBarName;
        [SerializeField] private Health.Health enemyHealth;
        [SerializeField] private Transform healthBarPos;
        
        private HealthView _healthView;
        private bool waitingOnUIObjectPooler = false;

        private void Start()
        {
            if (waitingOnUIObjectPooler)
            {
                SetUpHealthBar();
            }
        }

        private void OnEnable()
        {
            waitingOnUIObjectPooler = UIObjectPooler.UIInstance == null;
            
            if (waitingOnUIObjectPooler)
            {
                return;
            }
            
            SetUpHealthBar();
        }

        private void SetUpHealthBar()
        {
            waitingOnUIObjectPooler = false;
            var healthBarObj = UIObjectPooler.UIInstance.GetPooledObject(healthBarName.Value);
            _healthView = healthBarObj.GetComponent<HealthView>();
            _healthView.SetUp(enemyHealth, healthBarPos);
            healthBarObj.SetActive(true);
        }

        private void OnDisable()
        {
            if (_healthView != null)
            {
                _healthView.gameObject.SetActive(false);
                _healthView = null;
            }
        }
    }
}
