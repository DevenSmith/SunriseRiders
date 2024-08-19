using System;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Devens
{
    public class SceneTransition : MonoBehaviour
    {
        public static SceneTransition Instance;
        
        [SerializeField] private RectTransform fader;
        [SerializeField] private FloatSO duration;
        public UnityEvent onTransitionFinished;

        public void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance);
            }

            Instance = this;
        }

        private void Start()
        {
            StartTransitionIn();
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        private void StartTransitionIn()
        {
            fader.gameObject.SetActive(true);
            fader.localScale = Vector3.one;

            fader.DOScale(Vector3.zero, duration.Value).OnComplete(() => 
            {
                fader.gameObject.SetActive(false);
                onTransitionFinished?.Invoke();
            });
        }

        [UsedImplicitly]
        public void StartTransitionOut()
        {
            fader.gameObject.SetActive(true);
            fader.localScale = Vector3.zero;

            fader.DOScale(Vector3.one, duration.Value).OnComplete(() => 
            {
                onTransitionFinished?.Invoke();
            });
        }
    }
}