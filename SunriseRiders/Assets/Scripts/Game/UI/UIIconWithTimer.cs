using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIIconWithTimer : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private Image fillBar;
        [SerializeField] private TMP_Text timeText;

        private float duration;
        private float timeRemaining;
        private string iconID;
    
        public void ShowIcon(Sprite icon, float duration, string id)
        {
            this.iconImage.sprite = icon;
            this.duration = duration;
            this.timeRemaining = duration;
            this.iconID = id;

            fillBar.fillAmount = 1.0f;
            timeText.text = Mathf.Ceil(timeRemaining).ToString() + "s";
        }

        private void Update()
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                fillBar.fillAmount = timeRemaining/duration;
                timeText.text = Mathf.Ceil(timeRemaining).ToString() + "s";

                if (timeRemaining <= 0)
                {
                    HideUI();
                }
            }
        }

        private void HideUI()
        {
            UIIconWithTimerManager.Instance.RemoveTimer(iconID);
            Destroy(gameObject);
        }
    }
}