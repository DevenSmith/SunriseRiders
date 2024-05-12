using Devens;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Effects
{
    public class ParticleSystemSpawner : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private bool spawnAsChild = true;
        [SerializeField] private GameObjectSO particleSystemPrefab;
        
        private void Awake()
        {
            if (spawnPoint == null)
                spawnPoint = transform;
        }

        [UsedImplicitly]
        public void SpawnParticles()
        {
            var obj = Instantiate(particleSystemPrefab.Value, spawnPoint.position, particleSystemPrefab.Value.transform.rotation);
            if (spawnAsChild)
            {
                obj.transform.SetParent(spawnPoint);
            }
        }
    }
}
