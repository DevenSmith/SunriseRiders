using JetBrains.Annotations;
using UnityEngine;

namespace Game.Effects
{
    public class ParticleSystemSpawner : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private bool spawnAsChild = true;
        [SerializeField] private GameObject particleSystemPrefab;
        
        private void Awake()
        {
            if (spawnPoint == null)
                spawnPoint = transform;
        }

        [UsedImplicitly]
        public void SpawnParticles()
        {
            var obj = Instantiate(particleSystemPrefab, spawnPoint.position, particleSystemPrefab.transform.rotation);
            if (spawnAsChild)
            {
                obj.transform.SetParent(spawnPoint);
            }
        }
    }
}
