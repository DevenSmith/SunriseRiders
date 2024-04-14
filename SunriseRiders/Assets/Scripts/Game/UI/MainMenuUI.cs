using DG.Tweening;
using UnityEngine;

namespace Game.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Transform titleTransform;
        [SerializeField] private float titleHeightDistance = 2.0f;
        [SerializeField] private float titleTweenDuration = 2.0f;
        
        public void Awake()
        {
            titleTransform.DOMoveY(titleHeightDistance, titleTweenDuration).From();
        }
    }
}
