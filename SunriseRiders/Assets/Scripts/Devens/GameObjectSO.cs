using UnityEngine;

namespace Devens
{
    [CreateAssetMenu (menuName = "Devens/GameObjectSO")]
    public class GameObjectSO : ScriptableObject
    {
        [SerializeField] private GameObject gameObjectValue;

        public GameObjectSO() { }

        public GameObject Value
        {
            get => gameObjectValue;
            set => gameObjectValue = value;
        }
    }
}
