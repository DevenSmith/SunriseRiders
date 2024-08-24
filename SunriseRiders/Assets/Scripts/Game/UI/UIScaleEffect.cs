using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.UI
{
    public class UIScaleEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public float scaleFactor = 1.2f;
        public float duration = 0.2f;

        private Vector3 originalScale;

        void Start()
        {
            originalScale = transform.localScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(originalScale * scaleFactor, duration);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(originalScale, duration);
        }
    }
}