using UnityEngine;

namespace Devens
{
    public abstract class ActionSO : ScriptableObject
    {
        public abstract void PerformAction(GameObject obj);
    }
}
