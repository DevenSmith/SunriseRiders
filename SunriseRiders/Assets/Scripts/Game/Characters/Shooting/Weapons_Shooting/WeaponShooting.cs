using Devens;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Characters.Shooting.Weapons_Shooting
{
    public class WeaponShooting : PausableMonoBehavior
    {
        [Header("Weapon Stats")]
        [SerializeField] protected FloatSO delayBetweenShots;
        [SerializeField] protected StringSO bulletPrefabName;
        [SerializeField] protected Transform bulletSpawnPoint;

        [Header("Model Set Up")] 
        [SerializeField] protected Transform leftHandRef;
        [SerializeField] protected Transform rightHandRef;

        [Header("Observable Stats")]
        [SerializeField] protected float shotDelay = 0.0f;
        
        public Transform LeftHandRef => leftHandRef;
        public Transform RightHandRef => rightHandRef;

        public UnityEvent onShoot;

        private void Update()
        {
            if (shotDelay > 0.0f)
            {
                shotDelay -= Time.deltaTime;
            }
        }
        
        public virtual bool Shoot()
        {
            if (shotDelay > 0.0f)
                return false;
            
            var bullet = ObjectPooler.Instance.GetPooledObject(bulletPrefabName.Value);
            bullet.SetActive(true);
            bullet.transform.position = bulletSpawnPoint.position;
            bullet.transform.rotation = bulletSpawnPoint.rotation;
            shotDelay = delayBetweenShots.Value;
            
            onShoot?.Invoke();
            return true;
        }
    }
}
