using System;
using Game.Characters.GameInput;
using UnityEngine;

namespace Game
{
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager Instance;

        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform playerTargetPoint;

        public Transform PlayerTransform => playerTransform;
        public Transform PlayerTargetPoint => playerTargetPoint;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            if (playerTransform == null)
            {
                playerTransform = FindObjectOfType<PlayerInput>().transform;

                if (playerTransform == null)
                {
                    Debug.LogError("No player set or found!");
                }
            }

            if (playerTargetPoint == null)
            {
                Debug.LogError("didn't assign the player target point to the enemy manager");
            }
        }
    }
}
