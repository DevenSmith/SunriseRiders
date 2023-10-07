using System.Collections.Generic;
using Game.Characters.Shooting;
using Game.Characters.Shooting.Weapons_Shooting;
using UnityEngine;

namespace Game.Characters
{
    public class WeaponSwapper : MonoBehaviour
    {
        [SerializeField] private List<WeaponShooting> weapons;
        [SerializeField] private int weaponIndex;

        [SerializeField] private PlayerShooting playerShooting;
        public void Update()
        {
            if (Input.GetButtonDown("NextWeapon"))
            {
                weaponIndex++;
                if (weaponIndex >= weapons.Count)
                {
                    weaponIndex = 0;
                }
                
                playerShooting.SetWeapon(weapons[weaponIndex]);
            }
        }
    }
}
