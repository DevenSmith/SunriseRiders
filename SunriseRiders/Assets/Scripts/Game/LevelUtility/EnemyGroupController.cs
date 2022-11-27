using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.LevelUtility
{
    /// <summary>
    /// Used to group objects to turn on when the player gets to a certain point
    /// </summary>
    public class EnemyGroupController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> groupEntitiesToTurnOn = new List<GameObject>();

        public void Start()
        {
            foreach (var groupObject in groupEntitiesToTurnOn)
            {
                groupObject.SetActive(false);
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject != GameManager.PlayerReference.characterObject)
            {
                return;
            }

            foreach (var groupObject in groupEntitiesToTurnOn)
            {
                groupObject.SetActive(true);
            }
            gameObject.SetActive(false);
        }
    }
}
