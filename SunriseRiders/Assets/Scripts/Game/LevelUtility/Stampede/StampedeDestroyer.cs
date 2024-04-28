using UnityEngine;

namespace Game.LevelUtility.Stampede
{
    public class StampedeDestroyer : MonoBehaviour
    {
        public void OnTriggerEnter(Collider other)
        {
            var stampede = other.GetComponent<StampedeDamager>();

            if (stampede != null)
            {
                Destroy(other.transform.parent.gameObject);
            }
        }
    }
}
