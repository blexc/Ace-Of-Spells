using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightningStrikeSpell : Spell
{
    float timeUntilShock = 1f;

    public override void InitSpell()
    {
        PlaceAtMousePos();
    }

    // doesn't get destroyed by triggers
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        //if hits an enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyHit = other.gameObject;
            var eb = enemyHit.GetComponent<EnemyBase>();

            DealDamageTo(eb);
        
            StartCoroutine(ShockEnemy(eb));
        }
    }

    IEnumerator ShockEnemy(EnemyBase eb)
    {
        yield return new WaitForSeconds(timeUntilShock);

        // apply shock to enemy (if not dead)
        if (eb) eb.AddStatusEffect(StatusEffect.Shock, effectlifeTime);
    }
}
