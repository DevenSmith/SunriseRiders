namespace Devens
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Devens/IntSO")]
    public class IntSO : ScriptableObject
    {
        [SerializeField] private int intValue;

        public IntSO() { }

        public IntSO(IntSO original)
        {
            Value = original.Value;
        }

        public int Value
        {
            get => intValue;
            set => intValue = value;
        }
    }
}
