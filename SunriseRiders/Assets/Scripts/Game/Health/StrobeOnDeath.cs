using System;
using System.Collections;
using System.Collections.Generic;
using Devens;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Health
{
    public class StrobeOnDeath : PausableMonoBehavior
    {
        [SerializeField] private Health health;
        [SerializeField] private FloatSO strobeDuration;
        [SerializeField] private FloatSO strobeRampUp;
        [SerializeField] private FloatSO strobeStartRate;

        [SerializeField] private List<GameObject> objectsToStrobe = new List<GameObject>();
        [SerializeField] private List<Collider> colliders = new List<Collider>();
        [SerializeField] private Rigidbody rigidBody;

        private Coroutine runningCoroutine;
        
        private void Awake()
        {
            if (health == null)
            {
                Debug.LogError("Health Not Set on DestroyOnDeathComponent of " + name);
                return;
            }
            
            health.onDie.AddListener(OnDeathAction);
        }
        
        private void OnDeathAction()
        {
            runningCoroutine = StartCoroutine(StrobeRoutine());
        }

        private void OnDestroy()
        {
            if (runningCoroutine != null)
            {
                StopCoroutine(runningCoroutine);
            }
        }

        private IEnumerator StrobeRoutine()
        {
            var remainingStrobeDuration = strobeDuration.Value;
            var strobeRate = strobeStartRate.Value;
            var remainingStrobeRate = strobeRate;
            
            SetListActiveState(!objectsToStrobe[0].activeInHierarchy);
            foreach (var collider in colliders)
            {
                collider.enabled = false;
            }
            rigidBody.isKinematic = true;
            rigidBody.useGravity = false;
            
            while (remainingStrobeDuration > 0.0f)
            {

                if (Paused)
                {
                    yield return new WaitForEndOfFrame();
                    continue;
                }

                remainingStrobeDuration -= Time.deltaTime;
                remainingStrobeRate -= Time.deltaTime;

                if (remainingStrobeRate <= 0)
                {
                    SetListActiveState(!objectsToStrobe[0].activeInHierarchy);
                    strobeRate *= strobeRampUp.Value;
                    remainingStrobeRate = strobeRate;
                }
                yield return new WaitForEndOfFrame();
            }
            gameObject.SetActive(false);
            yield break;
        }

        private void SetListActiveState(bool activeState)
        {
            foreach (var obj in objectsToStrobe)
            {
                obj.SetActive(activeState);
            }
        }
    }
}
