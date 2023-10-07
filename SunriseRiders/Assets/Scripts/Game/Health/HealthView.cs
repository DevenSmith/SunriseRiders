using Devens;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Health
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        
        private Health _health;
        private Transform _gamePositionTransform;
        private Camera _gameCamera;
        private RectTransform _healthViewTransform;
        private Canvas _canvas;
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
            UIObjectPooler.UIInstance.PoolObject(gameObject);
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
            healthSlider.value = (float)_health.CurrentHealth / (float)_health.StartingHealth;
        }

        private void HideHealthAmount()
        {
            healthSlider.value = 0;
            gameObject.SetActive(false);
        }
        

        private void LateUpdate()
        {
            var screenPos = _gameCamera.WorldToScreenPoint(_gamePositionTransform.position);
            float h = Screen.height;
            float w = Screen.width;
            var x = screenPos.x - (w / 2);
            var y = screenPos.y - (h / 2);
            _healthViewTransform.anchoredPosition = new Vector2(x, y);
        }


    }
}
