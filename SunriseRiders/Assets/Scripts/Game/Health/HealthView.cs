using Devens;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Health
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private float maxDuration = 1.0f;
        
        private Health _health;
        private Transform _gamePositionTransform;
        private Camera _gameCamera;
        private RectTransform _healthViewTransform;
        private Canvas _canvas;

        [SerializeField] private bool doesntMove = false;
        [SerializeField] private bool shouldPool = true;
        [SerializeField] private bool canBeDisabled = true;
        private void Awake()
        {
            _healthViewTransform = gameObject.GetComponent<RectTransform>();
        }

        private void Start()
        {
            if (_gameCamera == null)
            {
                _gameCamera = Camera.main;
            }

            _canvas = GetComponentInParent<Canvas>();
        }

        private void OnDisable()
        {
            _health = null;
            _gamePositionTransform = null;
            gameObject.SetActive(false);
            if (shouldPool)
            {
                UIObjectPooler.UIInstance.PoolObject(gameObject);
            }
        }

        public void SetUp(Health healthValue, Transform transformValue)
        {
            _health = healthValue;
            UpdateHealthAmount();
            _health.onHurt.AddListener(UpdateHealthAmount);
            _health.onDie.AddListener(HideHealthAmount);
            _gamePositionTransform = transformValue;
        }

        private void UpdateHealthAmount()
        {
            var newValue = (float) _health.CurrentHealth / (float) _health.StartingHealth;
            var change = Mathf.Abs(healthSlider.value - newValue);
            DOTween.To(() => healthSlider.value, x => healthSlider.value = x, newValue, maxDuration * change);
            
            //healthSlider.value = (float)_health.CurrentHealth / (float)_health.StartingHealth;
        }

        private void HideHealthAmount()
        {
            healthSlider.value = 0;

            if (canBeDisabled)
            {
                gameObject.SetActive(false);
            }
        }
        

        private void LateUpdate()
        {
            if (doesntMove)
                return;
            
            var screenPos = _gameCamera.WorldToScreenPoint(_gamePositionTransform.position);
            float h = Screen.height;
            float w = Screen.width;
            var x = screenPos.x - (w / 2);
            var y = screenPos.y - (h / 2);
            _healthViewTransform.anchoredPosition = new Vector2(x, y);
        }


    }
}
