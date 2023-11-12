using System;
using System.Collections;
using Devens;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Effects
{
    public class ExplosionBlast : PausableMonoBehavior
    {
        public MinMax<float> scaleRange;
        public MinMax<float> explosionTime;

        private Coroutine explodingRoutine;
        public void OnEnable()
        {
            if (explodingRoutine != null)
            {
                StopCoroutine(explodingRoutine);
            }

            explodingRoutine = StartCoroutine(ExplosionRoutine());
        }

        public void OnDisable()
        {
            if (explodingRoutine != null)
            {
                StopCoroutine(explodingRoutine);
            }
            ObjectPooler.Instance.PoolObject(gameObject);
        }

        private IEnumerator ExplosionRoutine()
        {
            var explosionDuration = Random.Range(explosionTime.Min, explosionTime.Max);
            var curExplosionTime = explosionDuration;
            var targetScale = Random.Range(scaleRange.Min, scaleRange.Max);

            while (curExplosionTime > 0.0f)
            {
                if (!Paused)
                {
                    curExplosionTime -= Time.deltaTime;
                    transform.localScale = Vector3.one * Mathf.Lerp(0, targetScale,
                        (explosionDuration - curExplosionTime) / explosionDuration);
                }

                yield return new WaitForEndOfFrame();
            }
            
            gameObject.SetActive(false);
            
            
            yield return null;
        }
    }
}
