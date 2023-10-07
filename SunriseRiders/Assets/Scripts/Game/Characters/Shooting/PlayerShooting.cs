using Game.Characters.GameInput;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Game.Characters.Shooting
{
   public class PlayerShooting : MonoBehaviour
   {
      [SerializeField] private RigBuilder rigBuilder;
      [SerializeField] private TwoBoneIKConstraint leftHandIK;
      [SerializeField] private TwoBoneIKConstraint rightHandIK;
      [SerializeField] private PlayerInput playerInput;

      [SerializeField] private Weapons_Shooting.WeaponShooting defaultWeapon;

      [SerializeField] private Weapons_Shooting.WeaponShooting currentWeapon;

      [SerializeField]private bool testingBuildRig = false;
      
      private void Start()
      {
         SetWeapon(currentWeapon == null ? defaultWeapon : currentWeapon);
      }

      public void SetWeapon(Weapons_Shooting.WeaponShooting weaponShooting)
      {
         if (currentWeapon != null)
         {
            currentWeapon.gameObject.SetActive(false);
         }

         currentWeapon = weaponShooting;
         leftHandIK.data.target = weaponShooting.LeftHandRef;
         rightHandIK.data.target = weaponShooting.RightHandRef;
         rigBuilder.Build();
         currentWeapon.gameObject.SetActive(true);
      }
      

      private void Update()
      {
         if (playerInput.shoot)
         {
            currentWeapon.Shoot();
         }

         if (testingBuildRig)
         {
            testingBuildRig = false;
            rigBuilder.Build();
         }
         
      }
   }
}
