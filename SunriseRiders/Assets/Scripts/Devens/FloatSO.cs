using System;

namespace Devens
{
    using UnityEngine;

    [CreateAssetMenu (menuName = "Devens/FloatSO")]
    public class FloatSO : ScriptableObject
    {
        [SerializeField] private float floatValue;

        public Action OnValueChanged;
        
        public FloatSO() { }

        public FloatSO(FloatSO original)
        {
            Value = original.Value;
        }

        public float Value
        {
            get => floatValue;
            set
            {
                if (value == floatValue)
                {
                    return;
                }

                floatValue = value;
                OnValueChanged?.Invoke();
            } 
        }
    }
}
