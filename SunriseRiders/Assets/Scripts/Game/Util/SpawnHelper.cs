using Devens;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Util
{
    public class SpawnHelper : MonoBehaviour
    {
        [SerializeField] private bool spawnAsChild = false;


        [UsedImplicitly]
        public void SpawnFromObjPool(StringSO prefabID)
        {
            var spawnedObj = ObjectPooler.Instance.GetPooledObject(prefabID.Value);

            if (spawnedObj == null)
            {
                Debug.LogError("SpawnedObj is null check if it is in object pool!");
                return;
            }
            
            spawnedObj.transform.position = transform.position;
            
            if (spawnAsChild)
            {
                spawnedObj.transform.SetParent(transform);
            }
            spawnedObj.SetActive(true);
        }
    }
}
