using UnityEngine;

namespace Game.Characters
{
    public class CharacterReference : MonoBehaviour
    {
        public GameObject characterObject;
        public Health.Health characterHealth;
        public Collider characterCollider;

        public void Awake()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            if (characterObject == null)
            {
                characterObject = gameObject;
            }

            if (characterHealth == null)
            {
                characterHealth = GetComponent<Health.Health>();
            }

            if (characterCollider == null)
            {
                characterCollider = GetComponent<Collider>();
            }
        }
    }
}
