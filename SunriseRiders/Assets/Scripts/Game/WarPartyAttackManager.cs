using System;
using Devens;
using Game.Signals;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Game
{
    public class WarPartyAttackManager : MonoBehaviour
    {
        [SerializeField] private IntSO minShotsToTrigger;
        [SerializeField] private IntSO maxShotsToTrigger;

        [SerializeField] private StringSO warPartyAttackPrefabName;
        [SerializeField] private FloatSO AttackPositionVariance;

        public UnityEvent onWarPartyAttackTriggered;
        
        [Header("Visible For Debug Purposes")]
        [SerializeField] private int shotsToTriggerRemaining = -1;

        private void Awake()
        {
            SetShotsToTrigger();
            Devens.Signals.Get<PlayShotSignal>().AddListener(OnPlayerShot);
        }

        private void OnDestroy()
        {
            Devens.Signals.Get<PlayShotSignal>().RemoveListener(OnPlayerShot);
        }

        private void OnPlayerShot()
        {
            shotsToTriggerRemaining--;

            if (shotsToTriggerRemaining <= 0)
            {
                SetShotsToTrigger();
                SpawnWarPartyAttack();
                onWarPartyAttackTriggered?.Invoke();
            }
        }

        private void SpawnWarPartyAttack()
        {
            var attacksToSpawn = Random.Range(1, 4);
            while (attacksToSpawn > 0)
            {
                attacksToSpawn--;
                var warPartyAttack = ObjectPooler.Instance.GetPooledObject(warPartyAttackPrefabName.Value);
                var playerPos = GameManager.PlayerReference.transform.position;
                playerPos.y = 0;
                playerPos.z = 0;
                playerPos.x += Random.Range(-AttackPositionVariance.Value, AttackPositionVariance.Value);
                warPartyAttack.transform.position = playerPos;
                warPartyAttack.SetActive(true);
            }
        }
        
        private void SetShotsToTrigger()
        {
            shotsToTriggerRemaining = Random.Range(minShotsToTrigger.Value, maxShotsToTrigger.Value);
        }
    }
}
