using System.Collections;
using System.Collections.Generic;
using Devens;
using Game;
using Game.Damage;
using Game.Health;
using Game.Health.ScriptableObjects;
using UnityEngine;

public class StampedeDamager : PausableMonoBehavior, IDamager
{
    [SerializeField] private IntSO stampedeDamage;
    [SerializeField] private List<DamageTypeSO> damageTypes;

    [SerializeField] private Collider stampedeGroundCollider;
    [SerializeField] private FloatSO groundOffTime;

    private Coroutine groundRoutine;
    public void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            DealDamage(damageable);
        }
    }

    public void DealDamage(IDamageable damageable)
    {
        if ((Health) damageable != GameManager.PlayerReference.characterHealth)
        {
            return;
        }
        
        damageable.TakeDamage(stampedeDamage.Value, damageTypes);
        GameManager.PlayerReference.playerInput.jump = true;

        stampedeGroundCollider.enabled = false;
        groundRoutine = StartCoroutine(TurnGroundBackOnRoutine());
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        if (groundRoutine != null)
        {
            StopCoroutine(groundRoutine);
        }
    }

    private IEnumerator TurnGroundBackOnRoutine()
    {
        var delay = groundOffTime.Value;
        while (Paused || delay > 0.0f)
        {
            yield return null;

            if (!Paused)
            {
                delay -= Time.deltaTime;
            }
        }

        stampedeGroundCollider.enabled = true;
    }
    
}
