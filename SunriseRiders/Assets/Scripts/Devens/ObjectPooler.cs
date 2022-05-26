using System;
using System.Collections.Generic;
using UnityEngine;

namespace Devens
{
    [Serializable]
    public class ObjectPoolItem {
        public int amountToPool;
        public GameObject objectToPool;
        public bool shouldExpand;
    }

    [Serializable]
    public class PoolItem
    {
        public GameObject objectPrefab;
        public Stack<GameObject> PooledObjects = new Stack<GameObject>();
        public bool shouldExpand = false;

        public PoolItem(GameObject prefab, bool expanding)
        {
            objectPrefab = prefab;
            shouldExpand = expanding;
        }
    }
    
    public class ObjectPooler : MonoBehaviour
    {
        public static ObjectPooler Instance;
        public List<ObjectPoolItem> itemsToPool;
        private Dictionary<string, PoolItem> _pooledObjects;
        public void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
        }

        private void Start () {
            _pooledObjects = new Dictionary<string, PoolItem>();
            foreach (var item in itemsToPool) {
                _pooledObjects.Add(item.objectToPool.name, new PoolItem(item.objectToPool, item.shouldExpand));
                for (var i = 0; i < item.amountToPool; i++) {
                    var obj = Instantiate(item.objectToPool, transform, true);
                    obj.name = item.objectToPool.name;
                    obj.SetActive(false);
                    _pooledObjects[item.objectToPool.name].PooledObjects.Push(obj);
                }
            }
        }
        
        /// <param name="key"> should refer to the game objects name</param>
        public GameObject GetPooledObject(string key) 
        {
            if (!_pooledObjects.ContainsKey(key)) 
                return null;
            
            var poolItem = _pooledObjects[key];
            
            if (poolItem.PooledObjects.Count > 0)
                return poolItem.PooledObjects.Pop();
            

            if (!poolItem.shouldExpand) 
                return null;
            
            var obj = Instantiate(poolItem.objectPrefab, transform, true);
            obj.name = poolItem.objectPrefab.name;
            obj.SetActive(false);
            return obj;
        }

        public void PoolObject(GameObject objectToPool)
        {
            if (_pooledObjects.ContainsKey(objectToPool.name))
            {
                _pooledObjects[objectToPool.name].PooledObjects.Push(objectToPool);
            }
            else
            {
                Debug.LogError($"Tried to pool object:{objectToPool.name} no object pool with name{objectToPool.name}");
            }
        }
    }
}
