using System;
using Devens;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Health
{
    public class PlayerDamagePulse : MonoBehaviour
    {
        [SerializeField] private FloatSO pulseDuration;

        [SerializeField] private Image imageToPulse;
        
        // Start is called before the first frame update
        void Start()
        {
            GameManager.PlayerReference.characterHealth.onHurt.AddListener(VisualPulse);
        }

        private void OnDestroy()
        {
            GameManager.PlayerReference.characterHealth.onHurt.RemoveListener(VisualPulse);
        }

        private void VisualPulse()
        {
            imageToPulse.enabled = true;
            imageToPulse.canvasRenderer.SetAlpha(1.0f);

            imageToPulse.CrossFadeAlpha(0, pulseDuration.Value, false);
        }
    }
}
