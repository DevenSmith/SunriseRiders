namespace Devens
{
    using UnityEngine;

    [CreateAssetMenu (menuName = "Devens/StringSO")]
    public class StringSO : ScriptableObject
    {
        [SerializeField] private string stringValue;

        public StringSO() { }

        public StringSO(StringSO original)
        {
            Value = original.Value;
        }

        public string Value
        {
            get => stringValue;
            set => stringValue = value;
        }
    }
}
