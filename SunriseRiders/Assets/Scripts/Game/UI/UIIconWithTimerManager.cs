using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class UIIconWithTimerManager : MonoBehaviour
    {
        [SerializeField] private UIIconWithTimer uiIconPrefab;
        [SerializeField] private Transform uiParent;
        
        private Dictionary<string, UIIconWithTimer> activeTimers = new Dictionary<string, UIIconWithTimer>();
        
        public static UIIconWithTimerManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public static void ShowTimerUI(Sprite icon, float duration, string uniqueID)
        {
            Instance.ShowTimer(icon, duration, uniqueID);
        }
        
        private void ShowTimer(Sprite icon, float duration, string uniqueID)
        {
            if (activeTimers.TryGetValue(uniqueID, out var existingTimer))
            {
                existingTimer.ShowIcon(icon, duration, uniqueID);
            }
            else
            {
                var newTimer = Instantiate(uiIconPrefab, uiParent);
                newTimer.ShowIcon(icon, duration, uniqueID);
                activeTimers.Add(uniqueID, newTimer); 
            }
        }

        
        public void RemoveTimer(string uniqueID)
        {
            if (activeTimers.ContainsKey(uniqueID))
            {
                var timer = activeTimers[uniqueID];
                activeTimers.Remove(uniqueID);
                Destroy(timer.gameObject);
            }
        }
    }
}
