using System.Collections.Generic;
using Devens;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class WarPartyAttack : PausableMonoBehavior
    {
        [SerializeField] private FloatSO delayBeforeArrows;
        [SerializeField] private List<Transform> attackSpawnPoints = new List<Transform>();
        [SerializeField] private StringSO arrowPrefabName;
        
        private float currentDelayBeforeArrows;

        public UnityEvent onArrowsSpawn;
        
        private void OnEnable()
        {
            if (delayBeforeArrows == null)
            {
                currentDelayBeforeArrows = 3.0f;
                Debug.LogWarning("WarPartyAttack needs a delay before arrows assigned!");
            }
            else
            {
                currentDelayBeforeArrows = delayBeforeArrows.Value;
            }
        }

        private void Update()
        {
            if (Paused)
            {
                return;
            }

            currentDelayBeforeArrows -= Time.deltaTime;

            if (currentDelayBeforeArrows > 0.0f)
            {
                return;
            }

            SpawnDamagers();

            ObjectPooler.Instance.PoolObject(gameObject);
            gameObject.SetActive(false);
        }

        private void SpawnDamagers()
        {
            if (arrowPrefabName != null)
            {
                if (attackSpawnPoints.Count == 0)
                {
                    Debug.LogWarning("WarPartyAttack has no assigned spawn points");
                }
                    
                foreach (var spawnPoint in attackSpawnPoints)
                {
                    var arrow = ObjectPooler.Instance.GetPooledObject(arrowPrefabName.Value);
                    arrow.transform.position = spawnPoint.position;
                    arrow.SetActive(true);
                }
                onArrowsSpawn?.Invoke();
            }
            else
            {
                Debug.LogWarning("WarPartyAttack is missing its arrow prefab name");
            }
        }
    }
}
