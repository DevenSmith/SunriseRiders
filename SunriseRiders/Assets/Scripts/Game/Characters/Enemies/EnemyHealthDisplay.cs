using System.Collections;
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

        private Coroutine waitForObjectPoolerRoutine;

        private void Start()
        {
            SetUpHealthView();
        }

        private void OnEnable()
        {
           // SetUpHealthView();
        }
        
        private void OnDisable()
        {
           // if (_healthView != null)
            //{
            //    _healthView.gameObject.SetActive(false);
            //    _healthView = null;
           // }
        }

        private void SetUpHealthView()
        {
            waitingOnUIObjectPooler = UIObjectPooler.UIInstance == null;
            
            if (waitingOnUIObjectPooler)
            {
                if (waitForObjectPoolerRoutine == null)
                {
                    waitForObjectPoolerRoutine = StartCoroutine(WaitForUIObjectPooler());
                }

                return;
            }
            SetUpHealthBar();
        }

        private IEnumerator WaitForUIObjectPooler()
        {
            if (waitingOnUIObjectPooler)
            {
                yield return new WaitForEndOfFrame();
                waitingOnUIObjectPooler = UIObjectPooler.UIInstance == null;
            }
            SetUpHealthBar();
            waitForObjectPoolerRoutine = null;
        }

        private void SetUpHealthBar()
        {
            if (_healthView == null)
            {
                waitingOnUIObjectPooler = false;
                var healthBarObj = UIObjectPooler.UIInstance.GetPooledObject(healthBarName.Value);
                _healthView = healthBarObj.GetComponent<HealthView>();
                _healthView.SetUp(enemyHealth, healthBarPos);
                healthBarObj.SetActive(true);
            }
            else
            {
                Debug.LogWarning(gameObject.name + " tried to set up a a health view and already had one");
            }
        }
    }
}
