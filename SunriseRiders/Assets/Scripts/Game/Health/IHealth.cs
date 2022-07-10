using System.Collections.Generic;
using Game.Health.ScriptableObjects;

namespace Game.Health
{
    public interface IHealth
    {
        void Heal(int amount);
        void Hurt(int amount);
    }
}
