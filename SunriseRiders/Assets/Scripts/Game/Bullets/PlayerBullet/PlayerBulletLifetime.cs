using Devens;
using UnityEngine;

namespace Game.Bullets.PlayerBullet
{
    /// <summary>
    /// Kill bullets after x seconds if they are still active otherwise they should die when they hit something.
    /// </summary>
    public class PlayerBulletLifetime : PausableMonoBehavior
    {
        [SerializeField] private FloatSO bulletLifetime;

        private float _lifetimeLeft = 0.0f;

        private void OnEnable()
        {
            _lifetimeLeft = bulletLifetime.Value;
        }

        private void Update()
        {
            if (Paused)
                return;
            
            _lifetimeLeft -= Time.deltaTime;
            if (_lifetimeLeft < 0.0f)
            {
                gameObject.SetActive(false);
                ObjectPooler.Instance.PoolObject(gameObject);
            }
        }
    }
}
