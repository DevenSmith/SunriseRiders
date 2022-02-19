namespace Devens
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Devens/BoolSO")]
    public class BoolSO : ScriptableObject
    {
        [SerializeField] private bool boolValue;

        public BoolSO() { }

        public BoolSO(BoolSO original)
        {
            Value = original.Value;
        }

        public bool Value
        {
            get => boolValue;
            set => boolValue = value;
        }
    }
}
