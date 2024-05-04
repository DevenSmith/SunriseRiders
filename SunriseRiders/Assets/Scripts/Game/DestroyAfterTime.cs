using UnityEngine;

namespace Game
{
    public class DestroyAfterTime : PausableMonoBehavior
    {
        [SerializeField] private float lifeTime = 2.0f;

        private void Update()
        {
            if (Paused)
                return;

            lifeTime -= Time.deltaTime;
            if (lifeTime < 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
