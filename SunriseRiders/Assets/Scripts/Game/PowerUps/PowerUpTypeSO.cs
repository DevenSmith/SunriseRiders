using System;
using UnityEngine;

namespace Game.PowerUps
{
    [Serializable]
    public class PowerUpTypeSO : ScriptableObject
    {
        public virtual void ApplyPowerUp(PowerUp caller)
        {
            Debug.LogWarning(name + ": didn't implement ApplyPowerUp()");
        }
    }
}

