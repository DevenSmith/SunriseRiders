using System;
using Devens;
using DG.Tweening;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class BossHealthDisplay : MonoBehaviour
    {
        [SerializeField] private Transform bossHealthMover;
        [SerializeField] private Transform showPosition; 
        [SerializeField] private Transform hidePosition;
        [SerializeField] private FloatSO duration;

        [SerializeField] private TMP_Text nameText;
        
        [SerializeField] private bool shouldStartHidden = true;

        [Header("Debug")] 
        [SerializeField] private bool testShow = false;
        [SerializeField] private bool testHide = false;
        [SerializeField] private bool testHideNow = false;
        
        private void Awake()
        {
            if (shouldStartHidden)
            {
                HideInstantly();
            }
        }

        public void Update()
        {
            if (testShow)
            {
                testShow = false;
                ShowBossHealth();
            }

            if (testHide)
            {
                testHide = false;
                HideBossHealth();
            }

            if (testHideNow)
            {
                testHideNow = false;
                HideInstantly();
            }
        }

        public void SetName(StringSO newName)
        {
            nameText.text = newName.Value;
        }

        [UsedImplicitly]
        public void HideInstantly()
        {
            bossHealthMover.position = hidePosition.position;
        }
        
        [UsedImplicitly]
        public void ShowBossHealth()
        {
            bossHealthMover.position = hidePosition.position;
            bossHealthMover.DOMove(showPosition.position, duration.Value);
        }

        [UsedImplicitly]
        public void HideBossHealth()
        {
            bossHealthMover.position = showPosition.position;
            bossHealthMover.DOMove(hidePosition.position, duration.Value);
        }
    }
}
