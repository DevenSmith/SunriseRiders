using System;
using UnityEngine;

namespace Game.Characters.Shooting
{
    public class WeaponModifiers : PausableMonoBehavior
    {
        public float FireRateModifier
        {
            get
            {
                if (fireRateModifierDuration > 0)
                {
                    return fireRateModifier;
                }

                return 1.0f;
            }
        }

        private float fireRateModifierDuration;
        private float fireRateModifier = 1.0f;

        public void UpdateFireRateModifier(float modifier, float duration)
        {
            fireRateModifier = modifier;
            fireRateModifierDuration = duration;
        }
        
        private void Update()
        {
            if (Paused)
                return;

            if (fireRateModifierDuration > 0)
            {
                fireRateModifierDuration -= Time.deltaTime;
            }
        }
    }
}
