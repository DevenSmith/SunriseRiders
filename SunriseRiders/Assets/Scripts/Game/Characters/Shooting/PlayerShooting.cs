using Devens;
using Game.Characters.GameInput;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Characters.Shooting
{
   public class PlayerShooting : MonoBehaviour
   {
      [SerializeField] private FloatSO delayBetweenShots;
      [SerializeField] private StringSO bulletPrefabName;
      [SerializeField] private PlayerInput playerInput;
      [SerializeField] private Transform bulletSpawnPoint;
      
      [SerializeField] private float shotDelay = 0.0f;

      private void Update()
      {
         if (shotDelay > 0.0f)
         {
            shotDelay -= Time.deltaTime;
         }
      
      
         if (shotDelay <= 0.0f && playerInput.shoot)
         {
            Shoot();
         }
      }

      private void Shoot()
      {
         var bullet = ObjectPooler.Instance.GetPooledObject(bulletPrefabName.Value);
         bullet.SetActive(true);
         bullet.transform.position = bulletSpawnPoint.position;
         bullet.transform.rotation = bulletSpawnPoint.rotation;
         shotDelay = delayBetweenShots.Value;
      }
   }
}
