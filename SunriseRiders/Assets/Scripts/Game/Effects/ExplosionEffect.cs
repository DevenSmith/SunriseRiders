using System;
using System.Collections;
using System.Collections.Generic;
using Devens;
using Devens.Utils;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Game.Effects
{
    public class ExplosionEffect : PausableMonoBehavior
    {
        [SerializeField] private bool triggerOnEnable = false;

        [SerializeField] private List<StringSO> explosionPrefabs;
        [SerializeField] private MinMax<float> mandatorySpawnInterval;
        [SerializeField] private List<Transform> mandatorySpawnPoints;

        [SerializeField] private float finishDelay;
    
        [Header("Optional Explosion Data")]
        [SerializeField] private OptionalExplosionType optionalExplosionType = OptionalExplosionType.Concurrent;
        [SerializeField, Tooltip("if empty will use main explosion prefab list")] private List<StringSO> optionalExplosionPrefabs;
        [SerializeField] private MinMax<int> optionalExplosions;
        [SerializeField] private MinMax<float> optionalExplosionInterval;
        [SerializeField] private List<Transform> optionalSpawnPoints;

        public UnityEvent onExplosionFinished;
    
        private Coroutine explosionRoutine;
    
        [Serializable]
        private enum OptionalExplosionType
        {
            BeforeMandatory,
            AfterMandatory,
            Concurrent
        }

        public void OnEnable()
        {
            if (!triggerOnEnable)
            {
                return;
            }
            TriggerExplosion();
        }

        public void TriggerExplosion()
        {
            if (explosionRoutine != null)
            {
                StopCoroutine(explosionRoutine);
            }

            explosionRoutine = StartCoroutine(ExplosionRoutine());
        }

        private IEnumerator ExplosionRoutine()
        {
            var curMandatoryTimer = 0.0f;
            var curOptionalTimer = 0.0f;

            var mandatoryRemaining = mandatorySpawnPoints.Count;
            var optionalRemaining = Random.Range(optionalExplosions.Min, optionalExplosions.Max);
        
            bool exploding = true;

            while (exploding)
            {
                if (!Paused)
                {
                    curMandatoryTimer -= Time.deltaTime;
                    curOptionalTimer -= Time.deltaTime;

                    if (optionalExplosionType == OptionalExplosionType.Concurrent)
                    {
                        if (curMandatoryTimer <= 0.0f)
                        {
                            SpawnMandatoryExplosion(mandatorySpawnPoints.Count - mandatoryRemaining);
                            mandatoryRemaining--;
                            curMandatoryTimer = Random.Range(mandatorySpawnInterval.Min, mandatorySpawnInterval.Max);

                            exploding = mandatoryRemaining > 0;
                        }

                        if (curOptionalTimer <= 0.0f && optionalRemaining > 0)
                        {
                            SpawnOptionalExplosion();
                            optionalRemaining--;
                            curOptionalTimer = Random.Range(optionalExplosionInterval.Min, optionalExplosionInterval.Max);
                        }
                    }
                    //TODO: implement logic for before and after settings
                }
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(finishDelay);
        
            onExplosionFinished?.Invoke();
        
            yield return null;
        }

        private void SpawnMandatoryExplosion(int position)
        {
            var explosionPrefab = explosionPrefabs[Random.Range(0, explosionPrefabs.Count)].Value;
            SpawnExplosion(explosionPrefab, mandatorySpawnPoints[position]);
        }

        private void SpawnOptionalExplosion()
        {
            var explosionPrefab = optionalExplosionPrefabs[Random.Range(0, optionalExplosionPrefabs.Count)].Value;
            var explosionParent = GetValidExplosionPosition(optionalSpawnPoints);
            SpawnExplosion(explosionPrefab, explosionParent);
        }

        private void SpawnExplosion(string prefabName, Transform parentTransform)
        {
            var explosion = ObjectPooler.Instance.GetPooledObject(prefabName);
            explosion.transform.SetParent(parentTransform);
            explosion.transform.localPosition = Vector3.zero;
            explosion.SetActive(true);
        }

        private Transform GetValidExplosionPosition(List<Transform> originalPositions)
        {
            var positions = new List<Transform>(originalPositions);
        
            if (positions.Count == 0)
            {
                return null;
            }

            positions.Shuffle();

            foreach (var position in positions)
            {
                if (position.childCount == 0)
                {
                    return position;
                }
            }

            return positions[0];
        }

    }
}
