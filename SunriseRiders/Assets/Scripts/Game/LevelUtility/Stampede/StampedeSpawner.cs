using System;
using System.Collections;
using System.Collections.Generic;
using Devens;
using Game.GameCamera;
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

        [SerializeField] private List<StampedeElement> StampedeElements = new List<StampedeElement>();
        [SerializeField] private float direction;
        [SerializeField] private Transform spawnPoint;
        
        private Coroutine stampedeSpawnRoutine;
        [SerializeField] private bool triggered = false;
        
        public void OnTriggerEnter(Collider other)
        {
            if (triggered || other.gameObject != GameManager.PlayerReference.characterObject)
            {
                return;
            }

            triggered = true;
            stampedeSpawnRoutine = StartCoroutine(SpawnStampede());
        }

        private IEnumerator SpawnStampede()
        {
            var shakeDur = 0.0f;
            foreach (var element in StampedeElements)
            {
                shakeDur += element.delayBeforeNextElement + 1.0f;
            }
            
            CameraController.Instance.ShakeCamera(shakeDur);
            
            var delay = 0.0f;
            foreach (var element in StampedeElements)
            {
                var obj = Instantiate(element.stampedeElementPrefab, spawnPoint);
                obj.GetComponent<StampedeMovement>().Initialize(element.speed.Value * direction);
                obj.transform.position = spawnPoint.position;

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
