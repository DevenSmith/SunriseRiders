using System;

namespace Devens
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Devens/IntSO")]
    public class IntSO : ScriptableObject
    {
        [SerializeField] private int intValue;

        public Action OnValueChanged;
        
        public IntSO() { }

        public IntSO(IntSO original)
        {
            Value = original.Value;
        }

        public int Value
        {
            get => intValue;
            set
            {
                if (value == intValue)
                {
                    return;
                }

                intValue = value;
                OnValueChanged?.Invoke();

            }
        }
    }
}
