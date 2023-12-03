using System;
using System.Collections.Generic;
using Devens;
using Game.Characters.Shooting;
using Game.Characters.Shooting.Weapons_Shooting;
using UnityEngine;

namespace Game.Characters
{
    public class WeaponSwapper : MonoBehaviour
    {
        [Serializable]
        public struct Weapon
        {
            public StringSO name;
            public WeaponShooting weaponShooting;
        }
        
        [SerializeField] private List<Weapon> weapons;
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
                
                playerShooting.SetWeapon(weapons[weaponIndex].weaponShooting);
            }
        }

        public void SetWeapon(int index)
        {
            weaponIndex = index;
            playerShooting.SetWeapon(weapons[weaponIndex].weaponShooting);
        }

        public int GetIndexOfWeapon(StringSO weaponToGetIndex)
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                if (weapons[i].name == weaponToGetIndex)
                {
                    return i;
                }
            }
            
            Debug.LogWarning( name + ": weapon not found in WeaponSwapper weapons");
            return -1;
        }
    }
}
