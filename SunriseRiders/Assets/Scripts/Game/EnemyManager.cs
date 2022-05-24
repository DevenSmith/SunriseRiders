using System;
using Game.Characters.GameInput;
using UnityEngine;

namespace Game
{
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager Instance;

        [SerializeField] private Transform playerTransform;

        public Transform PlayerTransform => playerTransform;

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
        }
    }
}
