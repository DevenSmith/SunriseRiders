using System.Collections.Generic;
using Game.Health.ScriptableObjects;

namespace Game.Damage
{
    public interface IDamageable
    {
        void TakeDamage(int amount, List<DamageTypeSO> damageTypes = null);
    }
}
