using Game.Characters.GameInput;
using Game.Signals;
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

      public Weapons_Shooting.WeaponShooting CurrentWeapon => currentWeapon != null? currentWeapon: defaultWeapon;

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

         SetupHand(weaponShooting.LeftHandRef, leftHandIK);
         SetupHand(weaponShooting.RightHandRef, rightHandIK);
         
         rigBuilder.Build();
         currentWeapon.gameObject.SetActive(true);
      }

      private void SetupHand(Transform handRef, TwoBoneIKConstraint handIK)
      {
         if (handRef != null)
         {
            handIK.weight = 1.0f;
            handIK.data.target = handRef;
         }
         else
         {
            handIK.weight = 0.0f;
            handIK.data.target = handRef;
         }
      }
      

      private void Update()
      {
         if (playerInput.shoot)
         {
            if (currentWeapon.Shoot())
            {
               Devens.Signals.Get<PlayShotSignal>().Dispatch();
            }
         }

         if (testingBuildRig)
         {
            testingBuildRig = false;
            rigBuilder.Build();
         }
         
      }
   }
}
