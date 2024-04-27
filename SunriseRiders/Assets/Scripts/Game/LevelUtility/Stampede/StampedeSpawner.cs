using System;
using System.Collections;
using System.Collections.Generic;
using Devens;
using UnityEngine;

namespace Game.LevelUtility.Stampede
{
    public class StampedeSpawner : PausableMonoBehavior
    {
        [System.Serializable]
        public class StampedeElement
        {
            public GameObject stampedeElementPrefab;
            public FloatSO speed;
            public float delayBeforeNextElement = 1.0f;
        }

        public List<StampedeElement> StampedeElements = new List<StampedeElement>();
        public float direction;
        public Transform spawnPoint;
        
        private Coroutine stampedeSpawnRoutine;
        private bool triggered = false;
        
        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject != GameManager.PlayerReference.characterObject)
            {
                return;
            }

            if (!triggered)
            {
                stampedeSpawnRoutine = StartCoroutine(SpawnStampede());
            }
        }

        private IEnumerator SpawnStampede()
        {
            var delay = 0.0f;
            foreach (var element in StampedeElements)
            {
                Instantiate(element.stampedeElementPrefab, spawnPoint).GetComponent<StampedeMovement>().Initialize(element.speed.Value * direction);

                while (Paused)
                {
                    yield return null;
                }

                delay = element.delayBeforeNextElement;

                while (delay > 0.0f)
                {
                    while (Paused)
                    {
                        yield return null;
                    }
                    
                    yield return null;
                    delay -= Time.deltaTime;
                }
            }
            yield return null;
        }
    }
}
