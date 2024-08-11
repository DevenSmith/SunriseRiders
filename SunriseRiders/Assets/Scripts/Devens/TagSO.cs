using UnityEngine;

namespace Devens
{
    [CreateAssetMenu (menuName = "Devens/TagSO")]
    public class TagSO : ScriptableObject
    {
        [SerializeField]
        private string tagName;

        public string TagName
        {
            get => tagName;
            set => tagName = value;
        }
    }
}